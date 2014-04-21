using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.BusinessLogic;
using AsyncTesting.DataAccess;

namespace AsyncTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataLayer = new DataLayer();
            var business = new BusinessLayer(dataLayer);
            var employees = Task.Run(() => business.GetEmployees());
            employees.Wait();
            foreach (var employee in employees.Result)
            {
                Console.WriteLine(string.Format("Employee Name: {0}, PLOT Status: {1}", employee.Person.Name, employee.Person.Description) );
            }
        }
    }
}
