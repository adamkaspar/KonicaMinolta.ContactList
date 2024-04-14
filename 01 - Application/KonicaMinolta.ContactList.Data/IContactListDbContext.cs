using Microsoft.EntityFrameworkCore;
using KonicaMinolta.Shared.Entities.Contacts;

namespace KonicaMinolta.ContactList.Data;

public interface IContactListDbContext
{
    public DbSet<Contact> Contacts { get; set; }
}
