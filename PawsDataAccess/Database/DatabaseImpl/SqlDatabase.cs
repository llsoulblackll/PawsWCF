using System.Data;
using System.Data.SqlClient;

namespace PawsDataAccess.Database.DatabaseImpl
{
    class SqlDatabase : IDatabase
    {
        /*public IDbConnection GetOpenConnection(string connString)
        {
            var conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }*/

        public IDbCommand GetCommand(string commandText, IDbConnection conn)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            return cmd;
        }

        public IDbCommand GetStoredProcedureCommand(string procedure, IDbConnection conn)
        {
            var cmd = conn.CreateCommand();//new SqlCommand(procedure, (SqlConnection)conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            return cmd;
        }

        public IDataParameter GetParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        public IDataParameter GetOutputParameter(string name, SqlDbType type, int size = 0)
        {
            //ASK FOR DBTYPE AND SIZE OTHER SIZE CANNOT BE 0 EXCEPTION IS THROWN
            var param = new SqlParameter();
            param.ParameterName = name;
            param.Direction = ParameterDirection.Output;
            param.SqlDbType = type;
            if (size > 0)
                param.Size = size;
            return param;
        }
    }
}
