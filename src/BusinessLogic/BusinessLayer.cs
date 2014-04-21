using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.DataAccess;
using AsyncTesting.Errors;
using AsyncTesting.Models;

namespace AsyncTesting.BusinessLogic
{
    public class BusinessLayer
    {
        public IDataLayer DataLayer { get; private set; }
        public BusinessLayer(IDataLayer dataLayer)
        {
            DataLayer = dataLayer;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            // Do some business logic-esque stuff, then fetch the data
            var employees = await DataLayer.GetEveryone();
            // Do some additional business logic-esque stuff and then return

            return employees;
        }

        public async Task<Employee> GetASpecificEmployee(int id)
        {
            var employee = await DataLayer.GetEmployeeById(id);
            // Do some business logic-esque stuff
            if (employee == null)
            {
                throw new EntityNotFoundException(string.Format("Get it together, chief. {0} doesn't exit.", id));
            }
            return employee;
        }
    }
}
