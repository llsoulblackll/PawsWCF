using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.Adoption;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class AdoptionDaoImpl : IAdoptionDao
    {
        private const string ID_COLUMN = "Id";
        private const string STATE_COLUMN = "State";
        private const string DESC_COLUMN = "Description";
        private const string ADDRESS_COLUMN = "Address";
        private const string AGE_COLUMN = "Age";
        private const string TOTAL_QUANTITY_COLUMN = "TotalQuantity";
        private const string AVAILABLE_QUANTITY_COLUMN = "AvailableQuantity";
        private const string PUBLISH_DATE_COLUMN = "PublishDate";
        private const string DISTRICT_ID_COLUMN = "DistrictId";
        private const string OWNER_ID_COLUMN = "OwnerId";
        private const string SPECIE_ID_COLUMN = "SpecieId";
        private const string RACE_ID_COLUMN = "RaceId";
        private const string PET_ID_COLUMN = "PetId";

        private const string ID_PARAM = "@id";
        private const string STATE_PARAM = "@state";
        private const string DESC_PARAM = "@desc";
        private const string ADDRESS_PARAM = "@address";
        private const string AGE_PARAM = "@age";
        private const string TOTAL_QUANTITY_PARAM = "@totalQuantity";
        private const string AVAILABLE_QUANTITY_PARAM = "@availableQuantity";
        private const string PUBLISH_DATE_PARAM = "@publishDate";
        private const string DISTRICT_ID_PARAM = "@disId";
        private const string OWNER_ID_PARAM = "@ownerId";
        private const string SPECIE_ID_PARAM = "@specieId";
        private const string RACE_ID_PARAM = "@raceId";
        private const string PET_ID_PARAM = "@petId";
        private const string ROW_COUNT_PARAM = "@rowCount";

        IDatabase db;
        IDbCommand cmd;
        IDataReader dr;

        public AdoptionDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(Adoption toInsert, IDbConnection conn)
        {
            using(cmd = db.GetStoredProcedureCommand(USP_ADOPTION_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.State)));
                cmd.Parameters.Add(db.GetParameter(DESC_PARAM, DaoUtil.ValueOrDbNull(toInsert.Description)));
                cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toInsert.Address)));
                cmd.Parameters.Add(db.GetParameter(AGE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Age)));
                cmd.Parameters.Add(db.GetParameter(TOTAL_QUANTITY_PARAM, DaoUtil.ValueOrDbNull(toInsert.TotalQuantity)));
                cmd.Parameters.Add(db.GetParameter(AVAILABLE_QUANTITY_PARAM, DaoUtil.ValueOrDbNull(toInsert.AvailableQuantity)));
                cmd.Parameters.Add(db.GetParameter(PUBLISH_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.PublishDate)));
                cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.DistrictId)));
                cmd.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.OwnerId)));
                cmd.Parameters.Add(db.GetParameter(SPECIE_ID_COLUMN, DaoUtil.ValueOrDbNull(toInsert.SpecieId)));
                cmd.Parameters.Add(db.GetParameter(RACE_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.RaceId)));
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.PetId)));
                cmd.Parameters.Add(db.GetOutputParameter(ID_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ID_PARAM]).Value;
            }
        }

        public bool Update(Adoption toUpdate, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_ADOPTION_UPDATE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Id)));
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.State)));
                //cmd.Parameters.Add(db.GetParameter(DESC_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Description)));
                //cmd.Parameters.Add(db.GetParameter(ADDRESS_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Address)));
                //cmd.Parameters.Add(db.GetParameter(AGE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Age)));
                //cmd.Parameters.Add(db.GetParameter(TOTAL_QUANTITY_PARAM, DaoUtil.ValueOrDbNull(toUpdate.TotalQuantity)));
                cmd.Parameters.Add(db.GetParameter(AVAILABLE_QUANTITY_PARAM, DaoUtil.ValueOrDbNull(toUpdate.AvailableQuantity)));
                //cmd.Parameters.Add(db.GetParameter(PUBLISH_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.PublishDate)));
                //cmd.Parameters.Add(db.GetParameter(DISTRICT_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.DistrictId)));
                //cmd.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OwnerId)));
                //cmd.Parameters.Add(db.GetParameter(RACE_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.RaceId)));
                //cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.PetId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_ADOPTION_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public Adoption Find(object id, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_ADOPTION_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                using (dr = cmd.ExecuteReader())
                {
                    int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);
                    int DESC_INDEX = dr.GetOrdinal(DESC_COLUMN);
                    int ADDRESS_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                    int AGE_INDEX = dr.GetOrdinal(AGE_COLUMN);
                    int TOTAL_QUANTITY_INDEX = dr.GetOrdinal(TOTAL_QUANTITY_COLUMN);
                    int AVAILABLE_QUANTITY_INDEX = dr.GetOrdinal(AVAILABLE_QUANTITY_COLUMN);
                    int PUBLISH_DATE_INDEX = dr.GetOrdinal(PUBLISH_DATE_COLUMN);
                    int DISTRICT_ID_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                    int OWNER_ID_INDEX = dr.GetOrdinal(OWNER_ID_COLUMN);
                    int SPECIE_ID_INDEX = dr.GetOrdinal(SPECIE_ID_COLUMN);
                    int RACE_ID_INDEX = dr.GetOrdinal(RACE_ID_COLUMN);
                    int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);

                    Adoption adop = null;

                    if (dr.Read())
                    {
                        adop = new Adoption
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr),
                            Description = DaoUtil.ValueOrDefault<string>(DESC_INDEX, dr),
                            Address = DaoUtil.ValueOrDefault<string>(ADDRESS_INDEX, dr),
                            Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dr),
                            TotalQuantity = DaoUtil.ValueOrDefault<int>(TOTAL_QUANTITY_INDEX, dr),
                            AvailableQuantity = DaoUtil.ValueOrDefault<int>(AVAILABLE_QUANTITY_INDEX, dr),
                            PublishDate = DaoUtil.ValueOrDefault<DateTime>(PUBLISH_DATE_INDEX, dr),
                            DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_INDEX, dr),
                            OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dr),
                            SpecieId = DaoUtil.ValueOrDefault<int>(SPECIE_ID_INDEX, dr),
                            RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dr),
                            PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr)
                        };
                    }

                    return adop;
                }
            }
        }

        public List<Adoption> FindAll(IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_ADOPTION_FINDALL, conn))
            using (dr = cmd.ExecuteReader())
            {
                int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);
                int DESC_INDEX = dr.GetOrdinal(DESC_COLUMN);
                int ADDRESS_INDEX = dr.GetOrdinal(ADDRESS_COLUMN);
                int AGE_INDEX = dr.GetOrdinal(AGE_COLUMN);
                int TOTAL_QUANTITY_INDEX = dr.GetOrdinal(TOTAL_QUANTITY_COLUMN);
                int AVAILABLE_QUANTITY_INDEX = dr.GetOrdinal(AVAILABLE_QUANTITY_COLUMN);
                int PUBLISH_DATE_INDEX = dr.GetOrdinal(PUBLISH_DATE_COLUMN);
                int DISTRICT_ID_INDEX = dr.GetOrdinal(DISTRICT_ID_COLUMN);
                int OWNER_ID_INDEX = dr.GetOrdinal(OWNER_ID_COLUMN);
                int SPECIE_ID_INDEX = dr.GetOrdinal(SPECIE_ID_COLUMN);
                int RACE_ID_INDEX = dr.GetOrdinal(RACE_ID_COLUMN);
                int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);

                List<Adoption> lAdoption = new List<Adoption>();
                Adoption adop = null;

                while (dr.Read())
                {
                    adop = new Adoption
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                        State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr),
                        Description = DaoUtil.ValueOrDefault<string>(DESC_INDEX, dr),
                        Address = DaoUtil.ValueOrDefault<string>(ADDRESS_INDEX, dr),
                        Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dr),
                        TotalQuantity = DaoUtil.ValueOrDefault<int>(TOTAL_QUANTITY_INDEX, dr),
                        AvailableQuantity = DaoUtil.ValueOrDefault<int>(AVAILABLE_QUANTITY_INDEX, dr),
                        PublishDate = DaoUtil.ValueOrDefault<DateTime>(PUBLISH_DATE_INDEX, dr),
                        DistrictId = DaoUtil.ValueOrDefault<int>(DISTRICT_ID_INDEX, dr),
                        OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dr),
                        SpecieId = DaoUtil.ValueOrDefault<int>(SPECIE_ID_INDEX, dr),
                        RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dr),
                        PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr)
                    };

                    lAdoption.Add(adop);
                }

                return lAdoption;
            }
        }
    }
}
