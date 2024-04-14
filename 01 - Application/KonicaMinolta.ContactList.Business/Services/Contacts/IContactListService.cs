using KonicaMinolta.Shared.Entities.Contacts;
using KonicaMinolta.Shared.Domain.Contacts;

namespace KonicaMinolta.ContactList.Business.Services.Contacts;

public interface IContactListService : IBaseService<Contact, ContactDto, ContactResultDto>
{
    List<ContactResultDto> GetAll(bool isActive);
}