using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.Race;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class RaceDaoImpl : IRaceDao
    {
        private const string ID_COLUMN = "Id";
        private const string NAME_COLUMN = "Name";
        private const string SPECIE_ID_COLUM = "SpecieId";

        private const string SPECIE_ID_PARAM = "@specieId";

        IDatabase db;
        //IDbCommand cmd;
        //IDataReader dr;

        public RaceDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(Race toInsert, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Update(Race toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public Race Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<Race> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException("FindAll requires the specieId, use the overloaded method instead");
        }

        public List<Race> FindAll(object specieId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_RACE_FINDALL, conn))
            {
                cmd.Parameters.Add(db.GetParameter(SPECIE_ID_PARAM, DaoUtil.ValueOrDbNull(specieId)));
                using (var dr = cmd.ExecuteReader())
                {
                    int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int NAME_INDEX = dr.GetOrdinal(NAME_COLUMN);
                    int SPECIE_ID_INDEX = dr.GetOrdinal(SPECIE_ID_COLUM);

                    List<Race> lRace = new List<Race>();
                    Race race;

                    while (dr.Read())
                    {
                        race = new Race
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                            Name = DaoUtil.ValueOrDefault<string>(NAME_INDEX, dr),
                            SpecieId = DaoUtil.ValueOrDefault<int>(SPECIE_ID_INDEX, dr)
                        };

                        lRace.Add(race);
                    }

                    return lRace;
                }
            }
        }
    }
}
