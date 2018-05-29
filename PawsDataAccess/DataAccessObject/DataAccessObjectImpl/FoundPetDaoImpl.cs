using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.FoundPet;
using PawsEntity;
using PawsDataAccess.Database;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class FoundPetDaoImpl : IFoundPetDao
    {
        private const string ID_COLUMN = "Id";
        private const string STATE_COLUMN = "State";
        private const string DESCRIPTION_COLUMN = "Description";
        private const string LONGITUDE_COLUMN = "Longitude";
        private const string LATITUDE_COLUMN = "Latitude";
        private const string FOUND_DATE_COLUMN = "FoundDate";
        private const string DELIVERED_DATE_COLUMN = "DeliveredDate";
        private const string ADDRESS_COLUMN = "Address";
        private const string DISTRICT_ID_COLUMN = "DistrictId";
        private const string RACE_ID_COLUMN = "RaceId";
        private const string FOUND_BY_ID_COLUMN = "FoundById";
        private const string DELIVERED_TO_ID_COLUMN = "DeliveredToId";

        private const string ID_PARAM = "@id";
        private const string STATE_PARAM = "@state";
        private const string DESCRIPTION_PARAM = "@desc";
        private const string LONGITUDE_PARAM = "@lon";
        private const string LATITUDE_PARAM = "@lat";
        private const string FOUND_DATE_PARAM = "@foundDate";
        private const string DELIVERED_DATE_PARAM = "@deliveredDate";
        private const string ADDRESS_PARAM = "@address";
        private const string DISTRICT_ID_PARAM = "@disId";
        private const string RACE_ID_PARAM = "@raceId";
        private const string FOUND_BY_ID_PARAM = "@foundById";
        private const string DELIVERED_TO_ID_PARAM = "@deliveredToId";
        private const string ROW_COUNT_PARAM = "@rowCount";

        IDatabase db;
        //IDbCommand cmd;
        //IDataReader dr;

        public FoundPetDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(FoundPet toInsert, IDbConnection conn)
        {
            using(var cmd = db.GetStoredProcedureCommand(USP_FOUND_PET_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.State)));
                cmd.Parameters.Add(db.GetParameter(DESCRIPTION_PARAM, DaoUtil.ValueOrDbNull(toInsert.Description)));
                cmd.Parameters.Add(db.GetParameter(LONGITUDE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Longitude)));
                cmd.Parameters.Add(db.GetParameter(LATITUDE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Latitude)));
                cmd.Parameters.Add(db.GetParameter(FOUND_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.FoundDate)));
                cmd.Parameters.Add(db.GetParameter(DELIVERED_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.DeliveredDate)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toInsert.Address)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.DistrictId)));
                cmd.Parameters.Add(db.GetParameter(RACE_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.RaceId)));
                cmd.Parameters.Add(db.GetParameter(FOUND_BY_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.FoundById)));
                cmd.Parameters.Add(db.GetParameter(DELIVERED_TO_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.DeliveredToId)));
                cmd.Parameters.Add(db.GetOutputParameter(ID_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ID_PARAM]).Value;
             }
        }

        public bool Update(FoundPet toUpdate, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_FOUND_PET_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Id)));
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.State)));
                cmd.Parameters.Add(db.GetParameter(DESCRIPTION_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Description)));
                cmd.Parameters.Add(db.GetParameter(LONGITUDE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Longitude)));
                cmd.Parameters.Add(db.GetParameter(LATITUDE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Latitude)));
                cmd.Parameters.Add(db.GetParameter(FOUND_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.FoundDate)));
                cmd.Parameters.Add(db.GetParameter(DELIVERED_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DeliveredDate)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Address)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DistrictId)));
                cmd.Parameters.Add(db.GetParameter(RACE_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.RaceId)));
                cmd.Parameters.Add(db.GetParameter(FOUND_BY_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.FoundById)));
                cmd.Parameters.Add(db.GetParameter(DELIVERED_TO_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DeliveredToId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_FOUND_PET_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public FoundPet Find(object id, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_FOUND_PET_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                using (var dr = cmd.ExecuteReader())
                {
                    int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);
                    int DESCRIPTION_INDEX = dr.GetOrdinal(DESCRIPTION_COLUMN);
                    int LONGITUDE_INDEX = dr.GetOrdinal(LONGITUDE_COLUMN);
                    int LATITUDE_INDEX = dr.GetOrdinal(LATITUDE_COLUMN);
                    int FOUND_DATE_INDEX = dr.GetOrdinal(FOUND_DATE_COLUMN);
                    int DELIVERED_DATE_INDEX = dr.GetOrdinal(DELIVERED_DATE_COLUMN);
                    int ADDRESS_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                    int DISTRICT_ID_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                    int RACE_ID_INDEX = dr.GetOrdinal(RACE_ID_COLUMN);
                    int FOUND_BY_ID_INDEX = dr.GetOrdinal(FOUND_BY_ID_COLUMN);
                    int DELIVERED_TO_ID_INDEX = dr.GetOrdinal(DELIVERED_TO_ID_COLUMN);

                    FoundPet foundPet = null;

                    if (dr.Read())
                    {
                        foundPet = new FoundPet
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr),
                            Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dr),
                            Longitude = DaoUtil.ValueOrDefault<double>(LONGITUDE_INDEX, dr),
                            Latitude = DaoUtil.ValueOrDefault<double>(LATITUDE_INDEX, dr),
                            FoundDate = DaoUtil.ValueOrDefault<DateTime>(FOUND_DATE_INDEX, dr),
                            DeliveredDate = DaoUtil.ValueOrDefault<DateTime>(DELIVERED_DATE_INDEX, dr),
                            Address = DaoUtil.ValueOrDefault<string>(ADDRESS_INDEX, dr),
                            DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_INDEX, dr),
                            RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dr),
                            FoundById = DaoUtil.ValueOrDefault<int>(FOUND_BY_ID_INDEX, dr),
                            DeliveredToId = DaoUtil.ValueOrDefault<int>(DELIVERED_TO_ID_INDEX, dr)
                        };
                    }

                    return foundPet;
                }
            }
        }

        public List<FoundPet> FindAll(IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_FOUND_PET_FINDALL, conn))
            using (var dr = cmd.ExecuteReader())
            {
                int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);
                int DESCRIPTION_INDEX = dr.GetOrdinal(DESCRIPTION_COLUMN);
                int LONGITUDE_INDEX = dr.GetOrdinal(LONGITUDE_COLUMN);
                int LATITUDE_INDEX = dr.GetOrdinal(LATITUDE_COLUMN);
                int FOUND_DATE_INDEX = dr.GetOrdinal(FOUND_DATE_COLUMN);
                int DELIVERED_DATE_INDEX = dr.GetOrdinal(DELIVERED_DATE_COLUMN);
                int ADDRESS_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                int DISTRICT_ID_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                int RACE_ID_INDEX = dr.GetOrdinal(RACE_ID_COLUMN);
                int FOUND_BY_ID_INDEX = dr.GetOrdinal(FOUND_BY_ID_COLUMN);
                int DELIVERED_TO_ID_INDEX = dr.GetOrdinal(DELIVERED_TO_ID_COLUMN);

                List<FoundPet> lFoundPet = new List<FoundPet>();
                FoundPet foundPet;

                while (dr.Read())
                {
                    foundPet = new FoundPet
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                        State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr),
                        Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dr),
                        Longitude = DaoUtil.ValueOrDefault<double>(LONGITUDE_INDEX, dr),
                        Latitude = DaoUtil.ValueOrDefault<double>(LATITUDE_INDEX, dr),
                        FoundDate = DaoUtil.ValueOrDefault<DateTime>(FOUND_DATE_INDEX, dr),
                        DeliveredDate = DaoUtil.ValueOrDefault<DateTime>(DELIVERED_DATE_INDEX, dr),
                        Address = DaoUtil.ValueOrDefault<string>(ADDRESS_INDEX, dr),
                        DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_INDEX, dr),
                        RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dr),
                        FoundById = DaoUtil.ValueOrDefault<int>(FOUND_BY_ID_INDEX, dr),
                        DeliveredToId = DaoUtil.ValueOrDefault<int>(DELIVERED_TO_ID_INDEX, dr)
                    };
                }

                return lFoundPet;
            }
        }
    }
}
