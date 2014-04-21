using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncTesting.Models;

namespace AsyncTesting.DataAccess
{
    public class DataLayer : IDataLayer
    {
        public string ConnectionString { get; set; }
        public DataLayer(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public DataLayer()
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AsyncTest"].ConnectionString;
        }

        public async Task<Person> GetPersonById(int id)
        {
            Person person = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select p.id, p.name, p.description from People as p where p.id=@id";

                    await connection.OpenAsync();

                    command.Parameters.Add(new SqlParameter("@id", id));

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                      person = new Person()
                      {
                          Id = reader.Get<int>("id"),
                          Description = reader.ReadString("Description"),
                          Name = reader.ReadString("Name")
                      };
                    }
                    reader.Close();
                }
                connection.Close();
            }

            return person;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select  e.id as employeeId, p.id as personId, p.Name, e.EmployeeNumber, p.Description from Employees as e inner join 
                                            People as p on e.PersonId = p.id where p.id=@id";

                    await connection.OpenAsync();

                    command.Parameters.Add(new SqlParameter("@id", id));

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        employee = new Employee()
                        {
                            Id = reader.Get<int>("employeeId"),
                            EmployeeNumber = reader.ReadString("EmployeeNumber"),
                            Person = new Person()
                            {
                                Id = reader.Get<int>("personId"), 
                                Description = reader.ReadString("Description"),
                                Name = reader.ReadString("Name") 
                            }
                        };
                    }
                    reader.Close();
                }
                connection.Close();
            }

            return employee;
        }

        /// <summary>
        /// EVERYOOOOOOOOONE!
        /// https://www.youtube.com/watch?v=MrTsuvykUZk
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetEveryone()
        {
            var everyone = new List<Employee>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"select  e.id as employeeId, p.id as personId, p.Name, e.EmployeeNumber, p.Description from Employees as e inner join 
                                            People as p on e.PersonId = p.id";

                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var employee = new Employee()
                        {
                            Id = reader.Get<int>("employeeId"),
                            EmployeeNumber = reader.ReadString("EmployeeNumber"),
                            Person = new Person()
                            {
                                Id = reader.Get<int>("personId"),
                                Description = reader.ReadString("Description"),
                                Name = reader.ReadString("Name")
                            }
                        };
                        everyone.Add(employee);
                    }
                    reader.Close();
                }
                connection.Close();
            }
          return everyone;
        }
    }
}
