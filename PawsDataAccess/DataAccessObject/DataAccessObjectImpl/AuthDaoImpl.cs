using System;
using System.Collections.Generic;
using System.Data;
using PawsDataAccess.Database;
using PawsEntity;
using static PawsDataAccess.Constant.Auth;

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

        public int Insert(Auth toInsert, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_AUTH_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(TOKEN_PARAM, DaoUtil.ValueOrDbNull(toInsert.Token)));
                cmd.Parameters.Add(db.GetParameter(CREATED_AT_PARAM, DaoUtil.ValueOrDbNull(toInsert.CreatedAt)));
                cmd.ExecuteNonQuery();
                return 0x22312;
            }
        }

        public bool Update(Auth toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            using (IDbCommand cmd = db.GetStoredProcedureCommand(USP_AUTH_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public Auth Find(object id, IDbConnection conn)
        {
            using (IDbCommand cmd = db.GetStoredProcedureCommand(USP_AUTH_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));

                using (IDataReader dr = cmd.ExecuteReader())
                {
                    int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int TOKEN_INDEX = dr.GetOrdinal(TOKEN_COLUMN);
                    int CREATED_AT_INDEX = dr.GetOrdinal(CREATED_AT_COLUMN);

                    Auth auth = null;

                    if (dr.Read())
                    {
                        auth = new Auth
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                            Token = DaoUtil.ValueOrDefault<string>(TOKEN_INDEX, dr),
                            CreatedAt = DaoUtil.ValueOrDefault<DateTime>(CREATED_AT_INDEX, dr)
                        };
                    }

                    return auth;
                }
            }
        }

        public List<Auth> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
