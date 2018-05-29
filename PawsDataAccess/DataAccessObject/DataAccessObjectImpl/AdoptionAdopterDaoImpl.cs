using System;
using System.Collections.Generic;
using System.Data;
using static PawsDataAccess.Constant.AdoptionAdopter;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class AdoptionAdopterDaoImpl : IAdoptionAdopterDao
    {

        private const string ADOPTION_ID_COLUMN = "AdoptionId";
        private const string ADOPTER_ID_COLUMN = "AdopterId";
        private const string ADOPTED_QUANTITY_COLUMN = "AdoptedQuantity";
        private const string ADOPTED_DATE_COLUMN = "AdoptedDate";

        private const string ADOPTION_ID_PARAM = "@adoptionId";
        private const string ADOPTER_ID_PARAM = "@adopterId";
        private const string ADOPTED_QUANTITY_PARAM = "@adoptedQuantity";
        private const string ADOPTED_DATE_PARAM = "@adoptedDate";
        private const string ROW_COUNT_PARAM = "@rowCount";

        IDatabase db;
        //IDbCommand cmd;
        //IDataReader dr;

        public AdoptionAdopterDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        //public int Insert(AdoptionAdopter toInsert, IDbConnection conn)
        //{
        //    throw new NotImplementedException("This insert does not return any id, use the boolean method instead");
        //}

        int IEntityDao<AdoptionAdopter>.Insert(AdoptionAdopter toInsert, IDbConnection conn)
        {
            throw new NotImplementedException("This insert does not return any id, use the boolean method instead");
        }

        public bool Insert(AdoptionAdopter toInsert, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_ADOPTION_ADOPTER_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdoptionId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdopterId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTED_QUANTITY_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdoptedQuantity)));
                cmd.Parameters.Add(db.GetParameter(ADOPTED_DATE_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdoptedDate)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;

            }
        }

        public bool Update(AdoptionAdopter toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException("Cannot update and AdoptionAdopter registry");
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException("Delete requires 2 ids to work, use the overloaded method instead");
        }

        public bool Delete(object adoptionId, object adopterId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_ADOPTION_ADOPTER_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, DaoUtil.ValueOrDbNull(adoptionId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(adopterId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;

            }
        }

        public AdoptionAdopter Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException("This method requires 2 ids to work, use the overloaded method instead");
        }

        public AdoptionAdopter Find(object adoptionId, object adopterId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_ADOPTION_ADOPTER_FIND, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, DaoUtil.ValueOrDbNull(adoptionId)));
                cmd.Parameters.Add(db.GetParameter(ADOPTER_ID_PARAM, DaoUtil.ValueOrDbNull(adopterId)));

                using (var dr = cmd.ExecuteReader())
                {
                    int ADOPTION_ID_INDEX= dr.GetOrdinal(ADOPTION_ID_COLUMN);
                    int ADOPTER_ID_INDEX = dr.GetOrdinal(ADOPTER_ID_COLUMN);
                    int ADOPTED_QUANTITY_INDEX = dr.GetOrdinal(ADOPTED_QUANTITY_COLUMN);
                    int ADOPTED_DATE_INDEX = dr.GetOrdinal(ADOPTED_DATE_COLUMN);

                    AdoptionAdopter adop = null;

                    if (dr.Read())
                    {
                        adop = new AdoptionAdopter
                        {
                            AdoptionId = DaoUtil.ValueOrDefault<int>(ADOPTION_ID_INDEX, dr),
                            AdopterId = DaoUtil.ValueOrDefault<int>(ADOPTER_ID_INDEX, dr),
                            AdoptedQuantity = DaoUtil.ValueOrDefault<int>(ADOPTED_QUANTITY_INDEX, dr),
                            AdoptedDate = DaoUtil.ValueOrDefault<DateTime>(ADOPTED_DATE_INDEX, dr)
                        };
                    }

                    return adop;
                }
            }
        }

        public List<AdoptionAdopter> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException("Adoption id is required for this method to work, use the overloaded one instead");
        }

        public List<AdoptionAdopter> FindAll(object adoptionId, IDbConnection conn)
        {
            using (var cmd = db.GetStoredProcedureCommand(USP_ADOPTION_ADOPTER_FINDALL, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, DaoUtil.ValueOrDbNull(adoptionId)));
                
                using (var dr = cmd.ExecuteReader())
                {
                    int ADOPTION_ID_INDEX = dr.GetOrdinal(ADOPTION_ID_COLUMN);
                    int ADOPTER_ID_INDEX = dr.GetOrdinal(ADOPTER_ID_COLUMN);
                    int ADOPTED_QUANTITY_INDEX = dr.GetOrdinal(ADOPTED_QUANTITY_COLUMN);
                    int ADOPTED_DATE_INDEX = dr.GetOrdinal(ADOPTED_DATE_COLUMN);

                    List<AdoptionAdopter> lAdoptionAdopter = new List<AdoptionAdopter>();
                    AdoptionAdopter adop = null;

                    while (dr.Read())
                    {
                        adop = new AdoptionAdopter
                        {
                            AdoptionId = DaoUtil.ValueOrDefault<int>(ADOPTION_ID_INDEX, dr),
                            AdopterId = DaoUtil.ValueOrDefault<int>(ADOPTER_ID_INDEX, dr),
                            AdoptedQuantity = DaoUtil.ValueOrDefault<int>(ADOPTED_QUANTITY_INDEX, dr),
                            AdoptedDate = DaoUtil.ValueOrDefault<DateTime>(ADOPTED_DATE_INDEX, dr)
                        };
                        lAdoptionAdopter.Add(adop);
                    }

                    return lAdoptionAdopter;
                }
            }
        }
    }
}
