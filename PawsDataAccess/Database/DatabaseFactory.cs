using PawsDataAccess.Database.DatabaseImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsDataAccess.Database
{
    static class DatabaseFactory
    {
        public static IDatabase GetSqlDatabase()
        {
            return new SqlDatabase();
        }
    }
}
