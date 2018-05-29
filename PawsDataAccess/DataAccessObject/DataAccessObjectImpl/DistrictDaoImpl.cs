using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.District;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class DistrictDaoImpl : IDistrictDao
    {
        private const string ID_COLUMN = "Id";
        private const string NAME_COLUMN = "Name";

        IDatabase db;
        //IDbCommand cmd;
        //IDataReader dr;

        public DistrictDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(District toInsert, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Update(District toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public District Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<District> FindAll(IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_DISTRICT_FINDALL, conn))
            using (var dr = cmd.ExecuteReader())
            {
                int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                int NAME_INDEX = dr.GetOrdinal(NAME_COLUMN);

                List<District> lDistrict = new List<District>();
                District dis;

                while (dr.Read())
                {
                    dis = new District
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                        Name = DaoUtil.ValueOrDefault<string>(NAME_INDEX, dr)
                    };

                    lDistrict.Add(dis);
                }

                return lDistrict;
            }
        }
    }
}
