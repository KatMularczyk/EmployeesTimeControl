using EmployeesTimeControl.Repositories;
using Moq;
using Xunit;
using EmployeesTimeControl.Controllers;
using Microsoft.AspNetCore.Mvc;
using EmployeesTimeControl.Models;

namespace EmployeeTimeControl.Tests.ControllerTests
{
    public class TimeEntriesControllerTests { 
    
        [Fact]
        public void GetEntries_Get_ReturnJsonWithEntries()
        {
            // Arrange
            var mockedRepo = new Mock<ITimeEntriesRepository>();
            int employeeId = 1;

            var expectedTimeEntries = new List<TimeEntry>
            {
            new TimeEntry { entryId = 1, employeeId = employeeId, date = "2024-12-01", hoursWorked = 10 },
            new TimeEntry { entryId = 2, employeeId = employeeId, date = "2024-12-02", hoursWorked = 8 }
            };

            mockedRepo.Setup(repository => repository.GetEntries(employeeId)).Returns(expectedTimeEntries);
            var controller = new TimeEntriesController(mockedRepo.Object);

            // Act
            var result = controller.GetEntries(employeeId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var actualEntries = Assert.IsType<List<TimeEntry>>(jsonResult.Value);
            Assert.Equal(expectedTimeEntries.Count, actualEntries.Count);
            Assert.Equal(expectedTimeEntries[0].entryId, actualEntries[0].entryId);
            Assert.Equal(expectedTimeEntries[1].hoursWorked, actualEntries[1].hoursWorked);
        }
    }
}
