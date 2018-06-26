using System;
using System.Collections.Generic;
using System.Data;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class AuthDaoImpl : IAuthDao
    {
        private const string ID_COLUMN = "Id";
        private const string TOKEN_COLUMN = "Token";
        private const string CREATED_AT_COLUMN = "CreatedAt";

        private const string ID_PARAM = "@id";
        private const string TOKEN_PARAM = "@token";
        private const string CREATED_AT_PARAM = "@createdAt";

        private const string ROW_COUNT_PARAM = "@rowCount";

        private IDatabase db = DatabaseFactory.GetSqlDatabase();

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public Auth Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<Auth> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public int Insert(Auth toInsert, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Update(Auth toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
