using Xunit;
using FluentAssertions;
using System.Net.Http.Json;
using KonicaMinolta.Shared.Domain.Contacts;

namespace KonicaMinolta.ContactList.Tests.IntegrationTests;

[Collection("IntegrationTests-Sequential")]
public class ContactListGetIntegrationTests : BaseIntegrationTest
{
    const string GetAllContactsUri = "api/v1/ContactList/GetAll";

    [Fact]
    public async Task GetAllContacts_ShouldReturn_AllContacts()
    {
        var result = await GetAsync<List<ContactResultDto>>(GetAllContactsUri);

        result
            .Should()
            .BeEquivalentTo(new List<ContactResultDto>
            {
                new ContactResultDto(1, "Adam", "Kaspar", 35, 123456, true),
                new ContactResultDto(2, "Petr", "Pavel", 65, 123456, true)
            });
    }

    [Fact]
    public async Task GetAllActiveContacts_ShouldReturn_AllActiveContacts()
    {
        var result = await GetAsync<List<ContactResultDto>>($"{GetAllContactsUri}/true");

        result
            .Should()
            .BeEquivalentTo(new List<ContactResultDto>
            {
                    new ContactResultDto(1, "Adam", "Kaspar", 35, 123456, true),
                    new ContactResultDto(2, "Petr", "Pavel", 65, 123456, true)
            });
    }

    [Fact]
    public async Task GetAllInActiveContacts_ShouldReturn_EmptyActiveContacts()
    {
        var result = await GetAsync<List<ContactResultDto>>($"{GetAllContactsUri}/false");

        result
            .Should()
            .BeEquivalentTo(new List<ContactResultDto>());
    }
}