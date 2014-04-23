using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.BusinessLogic;
using AsyncTesting.DataAccess;
using AsyncTesting.Errors;
using AsyncTesting.Models;
using Moq;
using NUnit.Framework;

namespace AsyncTesting.Test
{
    [TestFixture]
    public class BusinessLayerTests
    {
        BusinessLayer businessLayer;
        DataLayer dataLayer;

        Mock<IDataLayer> mockedDataLayer;
        BusinessLayer businessLayerWithMock;
 


        [SetUp]
        public void SetUp()
        {
            dataLayer = new DataLayer(); 
            businessLayer = new BusinessLayer(dataLayer);
            mockedDataLayer = new Mock<IDataLayer>();
            businessLayerWithMock = new BusinessLayer(mockedDataLayer.Object);
        }

        // STEP 1: Actually execute task, using await in test code
        [Test]
        public async void ItShouldActuallyExecuteTheTaskWhenCallingGetASpecificEmployee()
        {
            var employee = await businessLayer.GetASpecificEmployee(1);
            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(1));
        }

        [Test, ExpectedException(typeof(EntityNotFoundException))]
        public async void ItShouldActuallyExecuteTheTaskAndThrowAnExceptionWhenCallingGetASpecificEmployee()
        {
            var employee = await businessLayer.GetASpecificEmployee(10); 
        }

        // STEP 2: Ghetto mock w/a delegate
        [Test]
        public async void ItShouldExecuteTheDelegateInsteadWhenCallingGetASpecificEmployee()
        {
            mockedDataLayer.Setup( d=>d.GetEmployeeById(It.IsAny<int>())).Returns(
               delegate ()
               {
                   var task = new Task<Employee>(GetStubEmployee);
                   task.Start();
                   return task;
               }
               );

            var employee = await businessLayerWithMock.GetASpecificEmployee(1);
            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(1));
            Assert.That(employee.Person.Name, Is.EqualTo("Test Name")); 
        }

        // STEP 3: Use Task.FromResult, Moq.ReturnsAsync like a civilized human being
        [Test]
        public async void ItShouldTestInACivilizedWayWithReturnsAsync()
        {
            mockedDataLayer.Setup(d => d.GetEmployeeById(It.IsAny<int>()))
                .ReturnsAsync(GetStubEmployee());

            var employee = await businessLayerWithMock.GetASpecificEmployee(1);
            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(1));
            Assert.That(employee.Person.Name, Is.EqualTo("Test Name")); 
        }

        // STEP 3: Use Task.FromResult, Moq.ReturnsAsync like a civilized human being
        [Test]
        public async void ItShouldTestInACivilizedWayWithTaskFromResult()
        {
            mockedDataLayer.Setup(d => d.GetEmployeeById(It.IsAny<int>()))
                .Returns(Task.FromResult<Employee>(GetStubEmployee()));

            var employee = await businessLayerWithMock.GetASpecificEmployee(1);
            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(1));
            Assert.That(employee.Person.Name, Is.EqualTo("Test Name"));
        }

        // Final pt. A, for completness' sake, add in the an explicit Run and use blocking 
        // Result for completion. Don't Do this. Error handling is more complex and this is
        // less explicit
        [Test]
        public void ItShouldTestWithARunAndBlockOnResult()
        {
            var employeeTask = Task.Run(() => businessLayer.GetASpecificEmployee(1));
            var employee = employeeTask.Result;

            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(1));
        }

        // Final pt. B, for completness' sake, add in the an explicit Run/Wait 
        // for task completion. Don't Do this. Error handling is more complex and this is
        // less explicit
        [Test]
        public void ItShouldTestWithARunAndWait()
        {
            var employeeTask = Task.Run(() => businessLayer.GetASpecificEmployee(1));
            employeeTask.Wait();
            var employee = employeeTask.Result;

            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(1)); 
        }

        public Employee GetStubEmployee()
        {
            var stubbedEmployee = new Employee();
            stubbedEmployee.Id = 1;
            stubbedEmployee.EmployeeNumber = "12345";
            stubbedEmployee.Person = new Person()
            {
                Name = "Test Name",
                Id = 1,
                Description = "A Test Description"
            };
            return stubbedEmployee; 
        }
    }
}
