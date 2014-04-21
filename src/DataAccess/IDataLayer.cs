using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.Models;

namespace AsyncTesting.DataAccess
{
    public interface IDataLayer
    {
        Task<Person> GetPersonById(int id);
        Task<Employee> GetEmployeeById(int id);
        Task<List<Employee>> GetEveryone();
    }
}
