using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.LostPet;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class LostPetDaoImpl : ILostPetDao
    {
        private const string ID_COLUMN = "Id";
        private const string STATE_COLUMN = "State";
        private const string DESCRIPTION_COLUMN = "Description";
        private const string AGE_COLUMN = "Age";
        private const string LONGITUDE_COLUMN = "Longitude";
        private const string LATITUDE_COLUMN = "Latitude";
        private const string LOST_DATE_COLUMN = "LostDate";
        private const string FOUND_DATE_COLUMN = "FoundDate";
        private const string ADDRESS_COLUMN = "Address";
        private const string DISTRICT_ID_COLUMN = "DistrictId";
        private const string OWNER_ID_COLUMN = "OwnerId";
        private const string FOUND_BY_ID_COLUMN = "FoundById";
        private const string PET_ID_COLUMN = "PetId";

        private const string ID_PARAM = "@id";
        private const string STATE_PARAM = "@state";
        private const string DESCRIPTION_PARAM = "@desc";
        private const string AGE_PARAM = "@age";
        private const string LONGITUDE_PARAM = "@lon";
        private const string LATITUDE_PARAM = "@lat";
        private const string LOST_DATE_PARAM = "@lostDate";
        private const string FOUND_DATE_PARAM = "@foundDate";
        private const string ADDRESS_PARAM = "@address";
        private const string DISTRICT_ID_PARAM = "@disId";
        private const string OWNER_ID_PARAM = "@ownerId";
        private const string FOUND_BY_ID_PARAM = "@foundById";
        private const string PET_ID_PARAM = "@petId";
        private const string ROW_COUNT_PARAM = "@rowCount";

        IDatabase db;
        //IDbCommand cmd;
        //IDataReader dr;

        public LostPetDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(LostPet toInsert, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_LOST_PET_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.State)));
                cmd.Parameters.Add(db.GetParameter(DESCRIPTION_PARAM, DaoUtil.ValueOrDbNull(toInsert.Description)));
                cmd.Parameters.Add(db.GetParameter(AGE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Age)));
                cmd.Parameters.Add(db.GetParameter(LONGITUDE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Longitude)));
                cmd.Parameters.Add(db.GetParameter(LATITUDE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Latitude)));
                cmd.Parameters.Add(db.GetParameter(LOST_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.LostDate)));
                cmd.Parameters.Add(db.GetParameter(FOUND_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.FoundDate)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toInsert.Address)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.DistrictId)));
                cmd.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.OwnerId)));
                cmd.Parameters.Add(db.GetParameter(FOUND_BY_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.FoundById)));
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.PetId)));
                cmd.Parameters.Add(db.GetOutputParameter(ID_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ID_PARAM]).Value;
            }
        }

        public bool Update(LostPet toUpdate, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_LOST_PET_UPDATE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Id)));
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.State)));
                cmd.Parameters.Add(db.GetParameter(DESCRIPTION_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Description)));
                cmd.Parameters.Add(db.GetParameter(AGE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Age)));
                cmd.Parameters.Add(db.GetParameter(LONGITUDE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Longitude)));
                cmd.Parameters.Add(db.GetParameter(LATITUDE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Latitude)));
                cmd.Parameters.Add(db.GetParameter(LOST_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.LostDate)));
                cmd.Parameters.Add(db.GetParameter(FOUND_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.FoundDate)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Address)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DistrictId)));
                cmd.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OwnerId)));
                cmd.Parameters.Add(db.GetParameter(FOUND_BY_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.FoundById)));
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.PetId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            using(var cmd = db.GetStoredProcedureCommand(USP_LOST_PET_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public LostPet Find(object id, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_LOST_PET_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));

                using (var dr = cmd.ExecuteReader())
                {
                    int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);
                    int DESCRIPTION_INDEX = dr.GetOrdinal(DESCRIPTION_COLUMN);
                    int AGE_INDEX = dr.GetOrdinal(AGE_COLUMN);
                    int LONGITUDE_INDEX = dr.GetOrdinal(LONGITUDE_COLUMN);
                    int LATITUDE_INDEX = dr.GetOrdinal(LATITUDE_COLUMN);
                    int LOST_DATE_INDEX = dr.GetOrdinal(LOST_DATE_COLUMN);
                    int FOUND_DATE_INDEX = dr.GetOrdinal(FOUND_DATE_COLUMN);
                    int ADDRESS_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                    int DISTRICT_ID_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                    int OWNER_ID_INDEX = dr.GetOrdinal(OWNER_ID_COLUMN);
                    int FOUND_BY_ID_INDEX = dr.GetOrdinal(FOUND_BY_ID_COLUMN);
                    int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);

                    LostPet lostPet = null;

                    if (dr.Read())
                    {
                        lostPet = new LostPet
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr),
                            Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dr),
                            Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dr),
                            Longitude = DaoUtil.ValueOrDefault<double>(LONGITUDE_INDEX, dr),
                            Latitude = DaoUtil.ValueOrDefault<double>(LATITUDE_INDEX, dr),
                            LostDate = DaoUtil.ValueOrDefault<DateTime>(LOST_DATE_INDEX, dr),
                            FoundDate = DaoUtil.ValueOrDefault<DateTime>(FOUND_DATE_INDEX, dr),
                            Address = DaoUtil.ValueOrDefault<string>(ADDRESS_INDEX, dr),
                            DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_INDEX, dr),
                            OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dr),
                            FoundById = DaoUtil.ValueOrDefault<int>(FOUND_BY_ID_INDEX, dr),
                            PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr)
                        };
                    }

                    return lostPet;
                }
            }
        }

        public List<LostPet> FindAll(IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_LOST_PET_FIND, conn))
            using (var dr = cmd.ExecuteReader())
            {
                int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);
                int DESCRIPTION_INDEX = dr.GetOrdinal(DESCRIPTION_COLUMN);
                int AGE_INDEX = dr.GetOrdinal(AGE_COLUMN);
                int LONGITUDE_INDEX = dr.GetOrdinal(LONGITUDE_COLUMN);
                int LATITUDE_INDEX = dr.GetOrdinal(LATITUDE_COLUMN);
                int LOST_DATE_INDEX = dr.GetOrdinal(LOST_DATE_COLUMN);
                int FOUND_DATE_INDEX = dr.GetOrdinal(FOUND_DATE_COLUMN);
                int ADDRESS_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                int DISTRICT_ID_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                int OWNER_ID_INDEX = dr.GetOrdinal(OWNER_ID_COLUMN);
                int FOUND_BY_ID_INDEX = dr.GetOrdinal(FOUND_BY_ID_COLUMN);
                int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);

                List<LostPet> lLostPet = new List<LostPet>();
                LostPet lostPet = null;

                while (dr.Read())
                {
                    lostPet = new LostPet
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                        State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr),
                        Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dr),
                        Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dr),
                        Longitude = DaoUtil.ValueOrDefault<double>(LONGITUDE_INDEX, dr),
                        Latitude = DaoUtil.ValueOrDefault<double>(LATITUDE_INDEX, dr),
                        LostDate = DaoUtil.ValueOrDefault<DateTime>(LOST_DATE_INDEX, dr),
                        FoundDate = DaoUtil.ValueOrDefault<DateTime>(FOUND_DATE_INDEX, dr),
                        Address = DaoUtil.ValueOrDefault<string>(ADDRESS_INDEX, dr),
                        DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_INDEX, dr),
                        OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dr),
                        FoundById = DaoUtil.ValueOrDefault<int>(FOUND_BY_ID_INDEX, dr),
                        PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr)
                    };
                    lLostPet.Add(lostPet);
                }

                return lLostPet;
            }
        }
    }
}
