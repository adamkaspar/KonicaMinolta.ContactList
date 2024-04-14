using KonicaMinolta.Shared.Entities.Contacts;

namespace KonicaMinolta.ContactList.Data.Repositories.Contacts;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(ContactListDbContext context) : base(context)
    { 
    }
}
