
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

namespace EmployeeTimeControl.Tests.ControllerTests
{
    public class EmployeeControllerTests
    {
        //private readonly EmployeeController _controller;

        [Fact]
        public void EmployeeController_Get_ReturnsJsonResultWithData()
        {
            // Arrange
            var mockedRepo = new Mock<IEmployeeRepository>();
            var expectedEmployeeList = new List<Employee>
            {
                new Employee { employeeId = 1, firstName = "Jan", lastName = "Kowalski", email = "jan.kowalski@example.com" },
                new Employee { employeeId = 2, firstName = "Anna", lastName = "Nowak", email = "anna.nowak@example.com" }
            };

            mockedRepo.Setup(repository => repository.GetEmployees()).Returns(expectedEmployeeList);
            var controller = new EmployeeController(mockedRepo.Object);

            // Act
            var returned = controller.Get();

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(returned);
            var actualEmployees = Assert.IsType<List<Employee>>(jsonResult.Value);
            Assert.Equal(expectedEmployeeList.Count, actualEmployees.Count);
            Assert.Equal(expectedEmployeeList[0].firstName, actualEmployees[0].firstName);

        }

    }
}
