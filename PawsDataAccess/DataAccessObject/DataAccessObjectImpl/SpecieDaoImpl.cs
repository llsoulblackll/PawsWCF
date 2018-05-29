using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.Specie;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class SpecieDaoImpl : ISpecieDao
    {
        private const string ID_COLUMN = "Id";
        private const string NAME_COLUMN = "Name";

        IDatabase db;
        //IDbCommand cmd;
        //IDataReader dr;

        public SpecieDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(Specie toInsert, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Update(Specie toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public Specie Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<Specie> FindAll(IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_SPECIE_FINDALL, conn))
            using (var dr = cmd.ExecuteReader())
            {
                int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                int NAME_INDEX = dr.GetOrdinal(NAME_COLUMN);

                List <Specie> lSpecie = new List<Specie>();
                Specie spe;
                while (dr.Read())
                {
                    spe = new Specie
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                        Name = DaoUtil.ValueOrDefault<string>(NAME_INDEX, dr)
                    };

                    lSpecie.Add(spe);
                }

                return lSpecie;
            }
        }
    }
}
