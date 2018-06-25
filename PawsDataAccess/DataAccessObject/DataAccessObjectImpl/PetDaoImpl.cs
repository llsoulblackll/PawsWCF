using System.Collections.Generic;
using System.Data;
using PawsDataAccess.Database;
using static PawsDataAccess.Constant.Pet;
using PawsEntity;
using System;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class PetDaoImpl : IPetDao
    {
        private const string ID_COLUMN = "Id";
        private const string NAME_COLUMN = "Name";
        private const string AGE_COLUMN = "Age";
        private const string DESCRIPTION_COLUMN = "Description";
        private const string PICTURE_COLUMN = "Picture";
        private const string PUBLISH_DATE_COLUMN = "PublishDate";
        private const string STATE_COLUMN = "State";
        private const string OTHER_RACE_COLUMN = "OtherRace";
        private const string SPECIE_ID_COLUMN = "SpecieId";
        private const string RACE_ID_COLUMN = "RaceId";
        private const string OWNER_ID_COLUMN = "OwnerId";

        private const string ID_PARAM = "@id";
        private const string NAME_PARAM = "@name";
        private const string AGE_PARAM = "@age";
        private const string DESCRIPTION_PARAM = "@desc";
        private const string PICTURE_PARAM = "@picture";
        private const string PUBLISH_DATE_PARAM = "@pubDate";
        private const string STATE_PARAM = "@state";
        private const string OTHER_RACE_PARAM = "@otherRace";
        private const string SPECIE_ID_PARAM = "@specieId";
        private const string RACE_ID_PARAM = "@raceId";
        private const string OWNER_ID_PARAM = "@ownerId";
        private const string ROW_COUNT_PARAM = "@rowCount";

        //private const string CONNECTION_STRING = "Data Source=.;Initial Catalog=FindMyBuddy;Integrated Security=True";

        private IDatabase db;

        //private IDbCommand command;
        //private IDataReader dataReader;

        public PetDaoImpl()
        {
            this.db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(Pet toInsert, IDbConnection conn)
        {
            using (var command = db.GetStoredProcedureCommand(USP_PET_INSERT, conn))
            {
                command.Parameters.Add(db.GetParameter(NAME_PARAM, DaoUtil.ValueOrDbNull(toInsert.Name)));
                command.Parameters.Add(db.GetParameter(AGE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Age)));
                command.Parameters.Add(db.GetParameter(DESCRIPTION_PARAM, DaoUtil.ValueOrDbNull(toInsert.Description)));
                command.Parameters.Add(db.GetParameter(PICTURE_PARAM, DaoUtil.ValueOrDbNull(toInsert.Picture)));
                command.Parameters.Add(db.GetParameter(PUBLISH_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.PublishDate)));
                command.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.State)));
                command.Parameters.Add(db.GetParameter(OTHER_RACE_PARAM, DaoUtil.ValueOrDbNull(toInsert.OtherRace)));
                command.Parameters.Add(db.GetParameter(SPECIE_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.SpecieId)));
                command.Parameters.Add(db.GetParameter(RACE_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.RaceId)));
                command.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.OwnerId)));
                //ADO.NET CANNOT INFERE PARAMETERS WITHOUT VALUE IN THIS CASE AN OUTPUT ONE, SO WE NEED TO SPECIFY IT
                //PROVIDE ONLY DBTYPE SINCE WE ARE NOT DEALING WITH VARCHAR OR CHAR INT HAS NOT DEFAULT VALUE OF 0
                command.Parameters.Add(db.GetOutputParameter(ID_PARAM, SqlDbType.Int));

                command.ExecuteNonQuery();

                int genId = (int)((IDataParameter)command.Parameters[ID_PARAM]).Value;

                return genId;
            }
        }

        public bool Update(Pet toUpdate, IDbConnection conn)
        {
            using (var command = db.GetStoredProcedureCommand(USP_PET_UPDATE, conn))
            {
                command.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Id)));
                command.Parameters.Add(db.GetParameter(NAME_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Name)));
                command.Parameters.Add(db.GetParameter(AGE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Age)));
                command.Parameters.Add(db.GetParameter(DESCRIPTION_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Description)));
                command.Parameters.Add(db.GetParameter(PICTURE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.Picture)));
                command.Parameters.Add(db.GetParameter(PUBLISH_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.PublishDate)));
                command.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.State)));
                command.Parameters.Add(db.GetParameter(OTHER_RACE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OtherRace)));
                command.Parameters.Add(db.GetParameter(SPECIE_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.SpecieId)));
                command.Parameters.Add(db.GetParameter(RACE_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.RaceId)));
                command.Parameters.Add(db.GetParameter(OWNER_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.OwnerId)));
                command.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));
                //ONLY WORKS WITH DML SENTENCES ON STORE PROCS RETURNS -1
                command.ExecuteNonQuery();
                //return command.ExecuteNonQuery() > 1;
                return (int)((IDataParameter)command.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            using (var command = db.GetStoredProcedureCommand(USP_PET_DELETE, conn))
            {
                command.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(id)));
                command.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));
                command.ExecuteNonQuery();
                return (int)((IDataParameter)command.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public Pet Find(object id, IDbConnection conn)
        {
            using (var command = db.GetStoredProcedureCommand(USP_PET_FIND, conn))
            {
                var param = command.CreateParameter();
                param.ParameterName = "@id";
                param.Value = id;
                param.DbType = DbType.Int32;
                command.Parameters.Add(DaoUtil.ValueOrDbNull(param));

                using (var dataReader = command.ExecuteReader())
                {
                    int ID_INDEX = dataReader.GetOrdinal(ID_COLUMN);
                    int NAME_INDEX = dataReader.GetOrdinal(NAME_COLUMN);
                    int AGE_INDEX = dataReader.GetOrdinal(AGE_COLUMN);
                    int DESCRIPTION_INDEX = dataReader.GetOrdinal(DESCRIPTION_COLUMN);
                    int PICTURE_INDEX = dataReader.GetOrdinal(PICTURE_COLUMN);
                    int PUBLISH_DATE_INDEX = dataReader.GetOrdinal(PUBLISH_DATE_COLUMN);
                    int STATE_INDEX = dataReader.GetOrdinal(STATE_COLUMN);
                    int OTHER_RACE_INDEX = dataReader.GetOrdinal(OTHER_RACE_COLUMN);
                    int SPECIE_ID_INDEX = dataReader.GetOrdinal(SPECIE_ID_COLUMN);
                    int RACE_ID_INDEX = dataReader.GetOrdinal(RACE_ID_COLUMN);
                    int OWNER_ID_INDEX = dataReader.GetOrdinal(OWNER_ID_COLUMN);

                    Pet p = null;
                    if (dataReader.Read())
                    {
                        p = new Pet
                        {
                            //Id = dataReader.IsDBNull(ID_INDEX) ? default(int) : dataReader.GetInt32(ID_INDEX),
                            //Name = dataReader.IsDBNull(NAME_INDEX) ? default(string) : dataReader.GetString(NAME_INDEX),
                            //Age = dataReader.IsDBNull(AGE_INDEX) ? default(int) : dataReader.GetInt32(AGE_INDEX),
                            //Description = dataReader.IsDBNull(DESCRIPTION_INDEX) ? default(string) : dataReader.GetString(DESCRIPTION_INDEX),
                            //Picture = dataReader.IsDBNull(PICTURE_INDEX) ? default(string) : dataReader.GetString(PICTURE_INDEX),
                            //RaceId = dataReader.IsDBNull(RACE_ID_INDEX) ? default(int) : dataReader.GetInt32(RACE_ID_INDEX),
                            //OwnerId = dataReader.IsDBNull(OWNER_ID_INDEX) ? default(int) : dataReader.GetInt32(OWNER_ID_INDEX)
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dataReader),
                            Name = DaoUtil.ValueOrDefault<string>(NAME_INDEX, dataReader),
                            Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dataReader),
                            Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dataReader),
                            Picture = DaoUtil.ValueOrDefault<string>(PICTURE_INDEX, dataReader),
                            PublishDate = DaoUtil.ValueOrDefault<DateTime>(PUBLISH_DATE_INDEX, dataReader),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dataReader),
                            OtherRace = DaoUtil.ValueOrDefault<string>(OTHER_RACE_INDEX, dataReader),
                            SpecieId = DaoUtil.ValueOrDefault<int>(SPECIE_ID_INDEX, dataReader),
                            RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dataReader),
                            OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dataReader)

                        };
                    }

                    return p;
                }
            }
        }

        public List<Pet> FindAll(IDbConnection conn)
        {
            using (var command = db.GetStoredProcedureCommand(USP_PET_FINDALL, conn))
            using (var dataReader = command.ExecuteReader())
            {
                int ID_INDEX = dataReader.GetOrdinal(ID_COLUMN);
                int NAME_INDEX = dataReader.GetOrdinal(NAME_COLUMN);
                int AGE_INDEX = dataReader.GetOrdinal(AGE_COLUMN);
                int DESCRIPTION_INDEX = dataReader.GetOrdinal(DESCRIPTION_COLUMN);
                int PICTURE_INDEX = dataReader.GetOrdinal(PICTURE_COLUMN);
                int PUBLISH_DATE_INDEX = dataReader.GetOrdinal(PUBLISH_DATE_COLUMN);
                int STATE_INDEX = dataReader.GetOrdinal(STATE_COLUMN);
                int OTHER_RACE_INDEX = dataReader.GetOrdinal(OTHER_RACE_COLUMN);
                int SPECIE_ID_INDEX = dataReader.GetOrdinal(SPECIE_ID_COLUMN);
                int RACE_ID_INDEX = dataReader.GetOrdinal(RACE_ID_COLUMN);
                int OWNER_ID_INDEX = dataReader.GetOrdinal(OWNER_ID_COLUMN);

                List<Pet> lPet = new List<Pet>();
                Pet p = null;
                while (dataReader.Read())
                {
                    p = new Pet
                    {
                        Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dataReader),
                        Name = DaoUtil.ValueOrDefault<string>(NAME_INDEX, dataReader),
                        Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dataReader),
                        Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dataReader),
                        Picture = DaoUtil.ValueOrDefault<string>(PICTURE_INDEX, dataReader),
                        PublishDate = DaoUtil.ValueOrDefault<DateTime>(PUBLISH_DATE_INDEX, dataReader),
                        State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dataReader),
                        OtherRace = DaoUtil.ValueOrDefault<string>(OTHER_RACE_INDEX, dataReader),
                        SpecieId = DaoUtil.ValueOrDefault<int>(SPECIE_ID_INDEX, dataReader),
                        RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dataReader),
                        OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dataReader)
                    };
                    lPet.Add(p);
                }

                return lPet;
            }
        }

        public List<Pet> FindAll(object ownerId, IDbConnection conn)
        {
            using (var command = db.GetStoredProcedureCommand(USP_PET_FINDALL, conn))
            {
                command.Parameters.Add(db.GetParameter(ID_PARAM, ownerId));
                using (var dataReader = command.ExecuteReader())
                {
                    int ID_INDEX = dataReader.GetOrdinal(ID_COLUMN);
                    int NAME_INDEX = dataReader.GetOrdinal(NAME_COLUMN);
                    int AGE_INDEX = dataReader.GetOrdinal(AGE_COLUMN);
                    int DESCRIPTION_INDEX = dataReader.GetOrdinal(DESCRIPTION_COLUMN);
                    int PICTURE_INDEX = dataReader.GetOrdinal(PICTURE_COLUMN);
                    int PUBLISH_DATE_INDEX = dataReader.GetOrdinal(PUBLISH_DATE_COLUMN);
                    int STATE_INDEX = dataReader.GetOrdinal(STATE_COLUMN);
                    int OTHER_RACE_INDEX = dataReader.GetOrdinal(OTHER_RACE_COLUMN);
                    int SPECIE_ID_INDEX = dataReader.GetOrdinal(SPECIE_ID_COLUMN);
                    int RACE_ID_INDEX = dataReader.GetOrdinal(RACE_ID_COLUMN);
                    int OWNER_ID_INDEX = dataReader.GetOrdinal(OWNER_ID_COLUMN);

                    List<Pet> lPet = new List<Pet>();
                    Pet p = null;
                    while (dataReader.Read())
                    {
                        p = new Pet
                        {
                            Id = DaoUtil.ValueOrDefault<int>(ID_INDEX, dataReader),
                            Name = DaoUtil.ValueOrDefault<string>(NAME_INDEX, dataReader),
                            Age = DaoUtil.ValueOrDefault<string>(AGE_INDEX, dataReader),
                            Description = DaoUtil.ValueOrDefault<string>(DESCRIPTION_INDEX, dataReader),
                            Picture = DaoUtil.ValueOrDefault<string>(PICTURE_INDEX, dataReader),
                            PublishDate = DaoUtil.ValueOrDefault<DateTime>(PUBLISH_DATE_INDEX, dataReader),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dataReader),
                            OtherRace = DaoUtil.ValueOrDefault<string>(OTHER_RACE_INDEX, dataReader),
                            SpecieId = DaoUtil.ValueOrDefault<int>(SPECIE_ID_INDEX, dataReader),
                            RaceId = DaoUtil.ValueOrDefault<int>(RACE_ID_INDEX, dataReader),
                            OwnerId = DaoUtil.ValueOrDefault<int>(OWNER_ID_INDEX, dataReader)
                        };
                        lPet.Add(p);
                    }

                    return lPet;
                }
            }
        }

        //CHANGE THIS FOR A SPECIALIZED SP
        public int Count(IDbConnection conn, int ownerId = 0)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_PET_FINDALL, conn))
            {
                if (ownerId > 0)
                    cmd.Parameters.Add(db.GetParameter(ID_PARAM, DaoUtil.ValueOrDbNull(ownerId)));
                using (var dr = cmd.ExecuteReader())
                {
                    int count = 0;
                    while (dr.Read())
                        count++;

                    return count;
                }
            }
        }
    }
}

