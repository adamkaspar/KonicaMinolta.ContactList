using Autofac;

namespace KonicaMinolta.ContactList.Data.IoC;

public class DataModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder
            .RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces();

        builder
            .RegisterType<ContactListDbContext>()
            .SingleInstance();
    }
}
