using Xunit;
using FluentAssertions;
using System.Net.Http.Json;
using KonicaMinolta.Shared.Domain.Contacts;

namespace KonicaMinolta.ContactList.Tests.IntegrationTests;

public class BaseIntegrationTest
{        
    protected const string BaseAddress = "http://localhost:5121/";

    protected readonly HttpClient httpClient;

    public BaseIntegrationTest()
    {
        if(httpClient is null)
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }        
    }

    protected async Task<T> GetAsync<T>(string uri)
    {
        var result = await httpClient.GetFromJsonAsync<T>(uri);

        return result;
    }

    protected async Task<T2> PostAsync<T1, T2>(string uri, T1 contactDto)
    {
        using var response = await httpClient.PostAsJsonAsync(uri, contactDto);
        var result = await response.Content.ReadFromJsonAsync<T2>();

        return result;
    }
}