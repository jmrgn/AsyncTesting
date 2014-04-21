using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTesting.DataAccess
{
    public static class SqlDataReaderExtensions
    {
        public static string ReadString(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);

            return reader.IsDBNull(ordinal) ? string.Empty : reader[ordinal].ToString().Trim();
        }

        public static T Get<T>(this SqlDataReader reader, string name)
        {
            var ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return default(T);
            }
            return (T)Convert.ChangeType(reader[ordinal], typeof(T));
        }
    }
}
