using AutoMapper;
using KonicaMinolta.Shared.Domain.Contacts;
using KonicaMinolta.Shared.Entities.Contacts;

namespace KonicaMinolta.ContactList.Api.Mappings;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactDto, Contact>();
        CreateMap<Contact, ContactResultDto>().ReverseMap();
    }
}
