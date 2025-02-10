
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
using EmployeesTimeControl.Repositories;
using EmployeesTimeControl.Models;
using EmployeesTimeControl.Services;

namespace EmployeeTimeControl.Tests.ControllerTests
{
    public class EmployeeControllerTests
    {

        [Fact]
        public void EmployeeController_Get_ReturnsJsonResultWithData()
        {
            // Arrange
            var mockedService = new Mock<IEmployeeService>();
            var expectedEmployeeList = new List<EmployeeDTO>
            {
                new EmployeeDTO { employeeId = 1, firstName = "Jan", lastName = "Kowalski", email = "jan.kowalski@example.com" },
                new EmployeeDTO { employeeId = 2, firstName = "Anna", lastName = "Nowak", email = "anna.nowak@example.com" }
            };

            mockedService.Setup(service => service.GetEmployeesDTO()).Returns(expectedEmployeeList);
            var controller = new EmployeeController(mockedService.Object);

            // Act
            var returned = controller.Get();

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(returned);
            var actualEmployees = Assert.IsType<List<EmployeeDTO>>(jsonResult.Value);
            Assert.Equal(expectedEmployeeList.Count, actualEmployees.Count);
            Assert.Equal(expectedEmployeeList[0].firstName, actualEmployees[0].firstName);

        }

    }
}
