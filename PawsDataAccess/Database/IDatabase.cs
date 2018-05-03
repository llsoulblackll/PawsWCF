using System.Data;

namespace PawsDataAccess.Database
{
    interface IDatabase
    {
        //IDbConnection GetOpenConnection(string connString);

        IDbCommand GetCommand(string commandText, IDbConnection conn);

        IDbCommand GetStoredProcedureCommand(string procedure, IDbConnection conn);

        IDataParameter GetParameter(string name, object value);

        IDataParameter GetOutputParameter(string name, SqlDbType type, int size = 0);

        /*private T GetDefaultValueIfDBNull<T>(int columnIndex, IDataReader reader)
        {
            return reader.IsDBNull(columnIndex) ? default(T) : reader.Get;
        }*/
    }
}
