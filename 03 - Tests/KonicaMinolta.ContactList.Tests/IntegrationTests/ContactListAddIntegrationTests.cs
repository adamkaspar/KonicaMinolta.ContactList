using Xunit;
using FluentAssertions;
using System.Net.Http.Json;
using KonicaMinolta.Shared.Domain.Contacts;

namespace KonicaMinolta.ContactList.Tests.IntegrationTests;

[Collection("IntegrationTests-Sequential")]
public class ContactListAddIntegrationTests : BaseIntegrationTest
{        
    const string AddContactUri = "api/v1/ContactList/Add";

    [Fact]
    public async Task AddContact_ShouldReturn_Contact()
    {
        var contact = new ContactDto("Petr", "Pavel", 65, 123456, true);

        var result = await PostAsync<ContactDto, ContactResultDto>(AddContactUri, contact);
        
        result
            .Should()
            .BeEquivalentTo(new ContactResultDto(2, "Petr", "Pavel", 65, 123456, true));
    }
}
