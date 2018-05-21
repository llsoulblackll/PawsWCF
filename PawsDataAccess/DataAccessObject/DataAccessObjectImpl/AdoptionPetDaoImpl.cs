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

        IDatabase db;
        IDbCommand cmd;
        IDataReader dr;

        public AdoptionPetDaoImpl()
        {
            db = DatabaseFactory.GetSqlDatabase();
        }

        public int Insert(AdoptionPet toInsert, IDbConnection conn)
        {
            using (cmd = db.GetCommand(USP_ADOPTION_PET_INSERT, conn))
            {
                cmd.Parameters.Add(db.GetParameter(ADOPTION_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.AdoptionId)));
                cmd.Parameters.Add(db.GetParameter(PET_ID_PARAM, DaoUtil.ValueOrDbNull(toInsert.PetId)));

                cmd.ExecuteNonQuery();

                //THIS OPERATION DOES NOT AUTOGENERATE AN ID
                return 0;
            }
        }

        public bool Update(AdoptionPet toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public AdoptionPet Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<AdoptionPet> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
