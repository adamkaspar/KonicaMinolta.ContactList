using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using KonicaMinolta.ContactList.Api.Mappings;
using KonicaMinolta.ContactList.Business.Services.Contacts;
using KonicaMinolta.ContactList.Data.Repositories.Contacts;
using KonicaMinolta.Shared.Entities.Contacts;
using KonicaMinolta.Shared.Domain.Contacts;


namespace KonicaMinolta.ContactList.Tests.UnitTests;

public class ContactListServiceUnitTests
{
    private readonly IContactListService contactListService;

    public ContactListServiceUnitTests()
    {
        //Mock AutoMapper
        var mapper = CreateMapper();

        //Mock ContactRepository
        var configurationRepository = CreateRepository();

        //ContactListService initialization
        contactListService = new ContactListService(configurationRepository.Object, mapper);
    }

    [Fact]
    public void GetAllContacts_ShouldReturn_AllContacts()
    {
        var result = contactListService.GetAll();

        result
            .Should()
            .BeEquivalentTo(new List<ContactResultDto>
            {
                new ContactResultDto(1, "Adam", "Kaspar", 35, 123456, true)
            });
    }

    [Fact]
    public void FindContactById_ShouldReturn_Contact()
    {
        var result = contactListService.FindById(1);

        result
            .Should()
            .BeEquivalentTo(new ContactResultDto(1, "Adam", "Kaspar", 35, 123456, true));
    }

    [Fact]
    public void FindContactById_ShouldReturn_Null()
    {
        var result = contactListService.FindById(2);

        result
            .Should()
            .BeNull();
    }

    private IMapper CreateMapper()
    {
        var mapperConfiguration = new MapperConfiguration(options => options.AddProfile(new ContactProfile()));
        var result = mapperConfiguration.CreateMapper();

        return result;
    }

    private Mock<IContactRepository> CreateRepository()
    {
        var result = new Mock<IContactRepository>();

        //GetAll
        result
            .Setup(repository => repository.GetAll())
            .Returns(new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    Name = "Adam",
                    Surname = "Kaspar",
                    Age = 35,
                    Phone = 123456,
                    IsActive = true
                }
            });

        //FindById(1)
        result
            .Setup(repository => repository.FindById(It.Is<int>(id => id == 1)))
            .Returns(new Contact
            {
                Id = 1,
                Name = "Adam",
                Surname = "Kaspar",
                Age = 35,
                Phone = 123456,
                IsActive = true
            });

        //FindById(2)
        result
            .Setup(repository => repository.FindById(It.Is<int>(id => id == 2)))
            .Returns((Contact)null);

        return result;
    }
}