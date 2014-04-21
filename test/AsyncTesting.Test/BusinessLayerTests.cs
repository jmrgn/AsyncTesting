using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.BusinessLogic;
using AsyncTesting.DataAccess;
using AsyncTesting.Errors;
using NUnit.Framework;

namespace AsyncTesting.Test
{
    [TestFixture]
    public class BusinessLayerTests
    {
        BusinessLayer businessLayer;
        DataLayer dataLayer;

        [SetUp]
        public void SetUp()
        {
            dataLayer = new DataLayer(); 
            businessLayer = new BusinessLayer(dataLayer);
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
        // STEP 3: Use Task.FromResult, Moq.ReturnsAsync a civilized human being
    }
}
