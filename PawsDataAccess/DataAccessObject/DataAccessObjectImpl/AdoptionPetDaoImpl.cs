using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsDataAccess.Database;
using PawsEntity;
using static PawsDataAccess.Constant.AdoptionPet;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class AdoptionPetDaoImpl : IAdoptionPetDao
    {
        private const string ADOPTION_ID_COLUMN = "AdoptionId";
        private const string PET_ID_COLUMN = "PetId";

        private const string ADOPTION_ID_PARAM = "@adoptionId";
        private const string PET_ID_PARAM = "@petId";

        private const string ROW_COUNT_PARAM = "@rowCount";

        IDatabase db;
        IDbCommand cmd;
        IDataReader dr;

        public AdoptionPetDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public bool Insert(AdoptionPet toInsert, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_ADOPTION_PET_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdoptionId)));
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.PetId)));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                //THIS OPERATION DOES NOT AUTOGENERATE AN ID
                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        int IEntityDao<AdoptionPet>.Insert(AdoptionPet toInsert, IDbConnection conn)
        {
            throw new NotImplementedException("No id is returned from this operation");
        }

        public bool Update(AdoptionPet toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException("Delete requires 2 ids");
        }

        public bool Delete(object adoptionId, object petId, IDbConnection conn)
        {
            using (cmd = db.GetStoredProcedureCommand(USP_ADOPTION_PET_DELETE, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, adoptionId));
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, petId));
                cmd.Parameters.Add(db.GetOutputParameter(ROW_COUNT_PARAM, SqlDbType.Int));

                cmd.ExecuteNonQuery();

                return (int)((IDataParameter)cmd.Parameters[ROW_COUNT_PARAM]).Value > 0;
            }
        }

        public AdoptionPet Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<AdoptionPet> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException("FindAll requires the adoption id");
        }

        public List<AdoptionPet> FindAll(object adoptionId, IDbConnection conn)
        {
            using (cmd = db.GetCommand(USP_ADOPTION_PET_FINDALL, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, adoptionId));
                using (dr = cmd.ExecuteReader())
                {
                    int ADOPTION_ID_INDEX = dr.GetOrdinal(ADOPTION_ID_COLUMN);
                    int PET_ID_INDEX = dr.GetOrdinal(PET_ID_COLUMN);

                    List<AdoptionPet> lAdoptionPet = new List<AdoptionPet>();
                    AdoptionPet adoptionPet;

                    while (dr.Read())
                    {
                        adoptionPet = new AdoptionPet
                        {
                            AdoptionId = DaoUtil.ValueOrDefault<int>(ADOPTION_ID_INDEX, dr),
                            PetId = DaoUtil.ValueOrDefault<int>(PET_ID_INDEX, dr)
                        };

                        lAdoptionPet.Add(adoptionPet);
                    }

                    return lAdoptionPet;
                }
            }
        }
    }
}
