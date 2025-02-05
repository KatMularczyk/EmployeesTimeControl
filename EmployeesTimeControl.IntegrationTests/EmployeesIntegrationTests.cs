using EmployeesTimeControl.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesTimeControl.IntegrationTests
{
    public class EmployeesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public EmployeesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetEmployees_ReturnsEmployeesData()
        {
            // Arrange


            // Act

            var returned = await _client.GetAsync("/api/Employee");

            // Assert 
            returned.EnsureSuccessStatusCode();
            var employees = await returned.Content.ReadFromJsonAsync<List<Employee>>();
            Assert.NotNull(employees);
            Assert.NotEmpty(employees);
        }


    }
}
