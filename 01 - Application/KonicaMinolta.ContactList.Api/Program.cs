using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using KonicaMinolta.ContactList.Api.IoC;
using KonicaMinolta.ContactList.Api.Middleware;
using KonicaMinolta.ContactList.Business.IoC;
using KonicaMinolta.ContactList.Business.Services.Contacts;
using KonicaMinolta.ContactList.Data.IoC;
using KonicaMinolta.ContactList.Data;

var builder = WebApplication.CreateBuilder(args);

//Add console log provider
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Konica Minolta Contact List Api",
        Description = "Api to support Contact List management."
    });
});

//Register AutoMapper as mapping framework
builder.Services.AddAutoMapper(typeof(ContactListService).Assembly);

// Register the DbContext
builder.Services.AddDbContext<ContactListDbContext>(options => 
    options.UseInMemoryDatabase("ContactList"));

//Register Autofac service provider factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

//Register Autofac modules
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ApiModule());
    builder.RegisterModule(new BusinessModule());
    builder.RegisterModule(new DataModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register global error handler
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
