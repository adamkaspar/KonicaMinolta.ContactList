using Microsoft.EntityFrameworkCore;
using KonicaMinolta.Shared.Entities.Contacts;

namespace KonicaMinolta.ContactList.Data;

public class ContactListDbContext : DbContext, IContactListDbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public ContactListDbContext(DbContextOptions<ContactListDbContext> options) : base(options) 
    { 
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Contact>()
            .HasData(
                new Contact
                {
                    Id = 1,
                    Name = "Adam",
                    Surname = "Kaspar",
                    Age = 35,
                    Phone = 123456,
                    IsActive = true
                }
            );
    }
}

