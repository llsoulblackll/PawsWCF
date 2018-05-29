using System;
using System.Collections.Generic;
using System.Data;
using PawsDataAccess.Database;
using PawsEntity;
using static PawsDataAccess.Constant.Survey;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class SurveyDaoImpl : ISurveyDao
    {
        private const string ID_COLUMN = "Id";
        private const string HOME_DESC_COLUMN = "HomeDescription";
        private const string AMOUNT_PEOPLE_COLUMN = "AmountOfPeople";
        private const string OTHER_PETS_COLUMN = "OtherPets";
        private const string OTHER_PETS_DESC_COLUMN = "OtherPetsDescription";
        private const string WORK_TYPE_COLUMN = "WorkType";
        private const string AVAILABILITY_COLUMN = "Availability";
        private const string OWNER_ID_COLUMN = "OwnerId";

        private const string ID_PARAM = "@id";
        private const string HOME_DESC_PARAM = "@homeDesc";
        private const string AMOUNT_PEOPLE_PARAM = "@people";
        private const string OTHER_PETS_PARAM = "@otherPets";
        private const string OTHER_PETS_DESC_PARAM = "@otherPetsDesc";
        private const string WORK_TYPE_PARAM = "@workType";
        private const string AVAILABILITY_PARAM = "@avail";
        private const string OWNER_ID_PARAM = "@ownerId";
        private const string ROW_COUNT_PARAM = "@rowCount";

        private IDatabase db;
        //private IDbCommand cmd;
        //private IDataReader dr;

        public SurveyDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(Survey toInsert, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_SURVEY_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(HOME_DESC_PARAM, DaoUtil.ValueOrDbNull(toInsert.HomeDescription)));
                cmd.Parameters.Add(db.GetParameter(AMOUNT_PEOPLE_PARAM, DaoUtil.ValueOrDbNull(toInsert.AmountOfPeople)));
                cmd.Parameters.Add(db.GetParameter(OTHER_PETS_PARAM, DaoUtil.ValueOrDbNull(toInsert.OtherPets)));
                cmd.Parameters.Add(db.GetParameter(OTHER_PETS_DESC_PARAM, DaoUtil.ValueOrDbNull(toInsert.OtherPetsDescription)));
                cmd.Parameters.Add(db.GetParameter(WORK_TYPE_PARAM, DaoUtil.ValueOrDbNull(toInsert.WorkType)));
                cmd.Parameters.Add(db.GetParameter(AVAILABILITY_PARAM, DaoUtil.ValueOrDbNull(toInsert.Availability)));
                cmd.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.OwnerId)));
                cmd.Parameters.Add(db.GetOutputParameter(ID_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ID_PARAM]).Value;
            }
        }

        public bool Update(Survey toUpdate, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_SURVEY_UPDATE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Id)));
                cmd.Parameters.Add(db.GetParameter(HOME_DESC_PARAM, DaoUtil.ValueOrDbNull(toUpdate.HomeDescription)));
                cmd.Parameters.Add(db.GetParameter(AMOUNT_PEOPLE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.AmountOfPeople)));
                cmd.Parameters.Add(db.GetParameter(OTHER_PETS_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OtherPets)));
                cmd.Parameters.Add(db.GetParameter(OTHER_PETS_DESC_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OtherPetsDescription)));
                cmd.Parameters.Add(db.GetParameter(WORK_TYPE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.WorkType)));
                cmd.Parameters.Add(db.GetParameter(AVAILABILITY_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Availability)));
                cmd.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OwnerId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public Survey Find(object id, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_SURVEY_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));

                using (var dr = cmd.ExecuteReader())
                {
                    int ID_INDEX = dr.GetOrdinal(ID_COLUMN);
                    int HOME_DESC_INDEX = dr.GetOrdinal(HOME_DESC_COLUMN);
                    int AMOUNT_PEOPLE_INDEX = dr.GetOrdinal(AMOUNT_PEOPLE_COLUMN);
                    int OTHER_PETS_INDEX = dr.GetOrdinal(OTHER_PETS_COLUMN);
                    int OTHER_PETS_DESC_INDEX = dr.GetOrdinal(OTHER_PETS_DESC_COLUMN);
                    int WORK_TYPE_INDEX = dr.GetOrdinal(WORK_TYPE_COLUMN);
                    int AVAILABILITY_INDEX = dr.GetOrdinal(AVAILABILITY_COLUMN);
                    int OWNER_ID_INDEX = dr.GetOrdinal(OWNER_ID_COLUMN);

                    Survey s = null;

                    if (dr.Read())
                    {
                        s = new Survey
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dr),
                            HomeDescription = DaoUtil.ValueOrDefault<string>(HOME_DESC_INDEX, dr),
                            AmountOfPeople = DaoUtil.ValueOrDefault<int>(AMOUNT_PEOPLE_INDEX, dr),
                            OtherPets = DaoUtil.ValueOrDefault<bool>(OTHER_PETS_INDEX, dr),
                            OtherPetsDescription = DaoUtil.ValueOrDefault<string>(OTHER_PETS_DESC_INDEX, dr),
                            WorkType = DaoUtil.ValueOrDefault<string>(WORK_TYPE_INDEX, dr),
                            Availability = DaoUtil.ValueOrDefault<string>(AVAILABILITY_INDEX, dr),
                            OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dr)
                        };
                    }

                    return s;
                }
            }
        }

        public List<Survey> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException();
        }

    }
}
