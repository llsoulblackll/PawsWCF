using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsDataAccess.Database;
using PawsEntity;
using static PawsDataAccess.Constant.PetAdopter;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class PetAdopterDaoImpl : IPetAdopterDao
    {
        private const string PET_ID_COLUMN = "PetId";
        private const string ADOPTER_ID_COLUMN = "AdopterId";
        private const string REQUEST_DATE_COLUMN = "RequestDate";
        private const string RESPONSE_DATE_COLUMN = "ResponseDate";
        private const string STATE_COLUMN = "State";

        private const string PET_ID_PARAM = "@petId";
        private const string ADOPTER_ID_PARAM = "@adopterId";
        private const string REQUEST_DATE_PARAM = "@reqDate";
        private const string RESPONSE_DATE_PARAM = "@resDate";
        private const string STATE_PARAM = "@state";

        private const string IS_ADOPTER_PARAM = "@isAdopter";
        private const string ROW_COUNT_PARAM = "@rowCount";

        private IDatabase db = DatabaseFactory.GetSqlDatabase();

        public int Insert(PetAdopter toInsert, IDbConnection conn)
        {
            using (IDbCommand cmd = db.GetStoredProcedureCommand(USP_PET_ADOPTER_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.PetId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdopterId)));
                cmd.Parameters.Add(db.GetParameter(REQUEST_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.RequestDate)));
                //NO RESPONSE DATE SINCE THAT HAPPENS AFTER INSERTION, WHEN THE USER DECIDES
                cmd.Parameters.Add(db.GetParameter(RESPONSE_DATE_PARAM, DBNull.Value));//DaoUtil.ValueOrDbNull(toInsert.ResponseDate)));
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.State)));

                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value;
            }
        }

        public bool Update(PetAdopter toUpdate, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_PET_ADOPTER_UPDATE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.PetId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(toUpdate.AdopterId)));
                cmd.Parameters.Add(db.GetParameter(REQUEST_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.RequestDate)));
                cmd.Parameters.Add(db.GetParameter(RESPONSE_DATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.ResponseDate)));
                cmd.Parameters.Add(db.GetParameter(STATE_PARAM, DaoUtil.ValueOrDbNull(toUpdate.State)));

                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public PetAdopter Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException("Use overloaded method instead");
        }

        public PetAdopter Find(object petId, object adopterId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_PET_ADOPTER_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(petId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(adopterId)));

                using (IDataReader dr = cmd.ExecuteReader())
                {
                    int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);
                    int ADOPTER_ID_INDEX = dr.GetOrdinal(ADOPTER_ID_COLUMN);
                    int REQUEST_DATE_INDEX = dr.GetOrdinal(REQUEST_DATE_COLUMN);
                    int RESPONSE_DATE_INDEX = dr.GetOrdinal(RESPONSE_DATE_COLUMN);
                    int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);

                    PetAdopter petAdopter = null;

                    if (dr.Read())
                    {
                        petAdopter = new PetAdopter
                        {
                            AdopterId = DaoUtil.ValueOrDefault<int>(ADOPTER_ID_INDEX, dr),
                            PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr),
                            RequestDate = DaoUtil.ValueOrDefault<DateTime>(REQUEST_DATE_INDEX, dr),
                            ResponseDate = DaoUtil.ValueOrDefault<DateTime>(RESPONSE_DATE_INDEX, dr),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr)
                        };
                    }

                    return petAdopter;
                }

            }
        }

        public List<PetAdopter> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException("Use overloaded method instead");
        }

        public List<PetAdopter> FindAllRequests(object adopterId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_PET_ADOPTER_FINDALL, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(adopterId)));
                cmd.Parameters.Add(db.GetParameter(IS_ADOPTER_PARAM, false));

                using (IDataReader dr = cmd.ExecuteReader())
                {
                    int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);
                    int ADOPTER_ID_INDEX = dr.GetOrdinal(ADOPTER_ID_COLUMN);
                    int REQUEST_DATE_INDEX = dr.GetOrdinal(REQUEST_DATE_COLUMN);
                    int RESPONSE_DATE_INDEX = dr.GetOrdinal(RESPONSE_DATE_COLUMN);
                    int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);

                    List<PetAdopter> lPetAdopter = new List<PetAdopter>();
                    PetAdopter petAdopter = null;

                    while (dr.Read())
                    {
                        petAdopter = new PetAdopter
                        {
                            AdopterId = DaoUtil.ValueOrDefault<int>(ADOPTER_ID_INDEX, dr),
                            PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr),
                            RequestDate = DaoUtil.ValueOrDefault<DateTime>(REQUEST_DATE_INDEX, dr),
                            ResponseDate = DaoUtil.ValueOrDefault<DateTime>(RESPONSE_DATE_INDEX, dr),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr)
                        };

                        lPetAdopter.Add(petAdopter);
                    }

                    return lPetAdopter;
                }
            }
        }

        public List<PetAdopter> FindAllAnswers(object adopterId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_PET_ADOPTER_FINDALL, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(adopterId)));
                cmd.Parameters.Add(db.GetParameter(IS_ADOPTER_PARAM, true));

                using (IDataReader dr = cmd.ExecuteReader())
                {
                    int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);
                    int ADOPTER_ID_INDEX = dr.GetOrdinal(ADOPTER_ID_COLUMN);
                    int REQUEST_DATE_INDEX = dr.GetOrdinal(REQUEST_DATE_COLUMN);
                    int RESPONSE_DATE_INDEX = dr.GetOrdinal(RESPONSE_DATE_COLUMN);
                    int STATE_INDEX = dr.GetOrdinal(STATE_COLUMN);

                    List<PetAdopter> lPetAdopter = new List<PetAdopter>();
                    PetAdopter petAdopter = null;

                    while (dr.Read())
                    {
                        petAdopter = new PetAdopter
                        {
                            AdopterId = DaoUtil.ValueOrDefault<int>(ADOPTER_ID_INDEX, dr),
                            PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr),
                            RequestDate = DaoUtil.ValueOrDefault<DateTime>(REQUEST_DATE_INDEX, dr),
                            ResponseDate = DaoUtil.ValueOrDefault<DateTime>(RESPONSE_DATE_INDEX, dr),
                            State = DaoUtil.ValueOrDefault<bool>(STATE_INDEX, dr)
                        };

                        lPetAdopter.Add(petAdopter);
                    }

                    return lPetAdopter;
                }
            }
        }
    }
}
