using AutoMapper;
using Microsoft.Extensions.Logging;
using KonicaMinolta.Shared.Domain.Contacts;
using KonicaMinolta.Shared.Entities.Contacts;
using KonicaMinolta.ContactList.Data.Repositories.Contacts;

namespace KonicaMinolta.ContactList.Business.Services.Contacts;

public class ContactListService : BaseService<Contact, ContactDto, ContactResultDto>, IContactListService
{    
    public ContactListService(IContactRepository contactRepository, IMapper mapper) : base(contactRepository, mapper)
    {        
    }

    public virtual List<ContactResultDto> GetAll(bool isActive) 
        => GetAllWithCondition(entity => entity.IsActive == isActive);
}