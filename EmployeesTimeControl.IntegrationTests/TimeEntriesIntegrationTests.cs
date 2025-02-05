using EmployeesTimeControl.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;


namespace EmployeesTimeControl.IntegrationTests
{
    public class TimeEntriesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TimeEntriesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> GetJwtTokenAsync()
        {
            var loginData = new
            {
                username = "Admin",
                password = "Admin123"
            };

            var response = await _client.PostAsJsonAsync("/api/Auth", loginData);
            response.EnsureSuccessStatusCode();
            var authResponse = await response.Content.ReadAsStringAsync(); 
            return !string.IsNullOrEmpty(authResponse) ? authResponse : throw new Exception("Nie udało się uzyskać tokena JWT");
        }

        [Fact]
        public async Task GetEntries_ReturnsTimeEntriesList()
        {
            // Arrange
            var token = await GetJwtTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var returned = await _client.GetAsync("/api/Employee/1/time-entries");

            // Assert 
            returned.EnsureSuccessStatusCode();
            var entries = await returned.Content.ReadFromJsonAsync<List<TimeEntry>>();
            Assert.NotNull(entries);
            Assert.NotEmpty(entries);
        }

    }
}
