using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsDataAccess.DataAccessObject
{
    static class DaoUtil
    {
        public static object ValueOrDbNull(object value)
        {
            if (value == null || value as int? == 0 || value as bool? == false || string.IsNullOrWhiteSpace(value.ToString()))
                return DBNull.Value;
            return value;
        }

        public static T ValueOrDefault<T>(int index, IDataReader dataReader)
        {
            var type = typeof(T);

            //return (T)Convert.ChangeType("", TypeCode.Int32);

            if (type == typeof(int))
                return (T)(object)(dataReader.IsDBNull(index) ? default(int) : dataReader.GetInt32(index));
            else if (type == typeof(string))
                return (T)(object)(dataReader.IsDBNull(index) ? default(string) : dataReader.GetString(index));
            else if (type == typeof(double))
                return (T)(object)(dataReader.IsDBNull(index) ? default(double) : dataReader.GetDouble(index));
            else if (type == typeof(DateTime))
                return (T)(object)(dataReader.IsDBNull(index) ? default(DateTime) : dataReader.GetDateTime(index));
            else if (type == typeof(bool))
                return (T)(object)(dataReader.IsDBNull(index) ? default(bool) : dataReader.GetBoolean(index));
            else if (type == typeof(decimal))
                return (T)(object)(dataReader.IsDBNull(index) ? default(decimal) : dataReader.GetDecimal(index));
            else if (type == typeof(float))
                return (T)(object)(dataReader.IsDBNull(index) ? default(float) : dataReader.GetFloat(index));
            else
                throw new InvalidCastException("Type not supported, yet");
        }
    }
}
