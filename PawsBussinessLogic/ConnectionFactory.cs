using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PawsDataAccess;

namespace PawsBussinessLogic
{
    static class ConnectionFactory
    {
        public static IDbConnection GetOpenConnecion()
        {
            IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constant.CONNECTION_STRING_KEY].ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
