using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTimeControl;
using EmployeesTimeControl.Controllers;
using Npgsql;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeControl.Tests.ControllerTests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly EmployeeController _controller;


        public EmployeeControllerTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c.GetConnectionString("EmployeesTCCon"))
                .Returns("Host=localhost;Database=testdb;Username=testuser;Password=testpass");

            _controller = new EmployeeController(_mockConfig.Object);
        }

        [Fact]
        public void EmployeeController_Get_ReturnsJsonResultWithData()
        {
            // Arrange
            var fakeTable = new DataTable();
            fakeTable.Columns.Add("Id", typeof(int));
            fakeTable.Columns.Add("Name", typeof(string));
            fakeTable.Rows.Add(1, "John Doe");

            var mockReader = new Mock<NpgsqlDataReader>();
            mockReader.Setup(r => r.Read()).Returns(true);
            mockReader.Setup(r => r.FieldCount).Returns(fakeTable.Columns.Count);

            var mockCommand = new Mock<NpgsqlCommand>();
            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);

            var mockConnection = new Mock<NpgsqlConnection>();
            mockConnection.Setup(con => con.Open());
            mockConnection.Setup(con => con.CreateCommand()).Returns(mockCommand.Object);

            // Act
            var result = _controller.Get();

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.NotNull(jsonResult.Value);
        }

    }
}
