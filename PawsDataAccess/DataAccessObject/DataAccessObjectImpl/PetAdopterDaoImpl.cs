using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsDataAccess.Database;
using PawsEntity;

namespace PawsDataAccess.DataAccessObject.DataAccessObjectImpl
{
    class PetAdopterDaoImpl : IPetAdopterDao
    {
        IDatabase db = DatabaseFactory.GetSqlDatabase();

        public int Insert(PawsEntity.PetAdopter toInsert, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Update(PawsEntity.PetAdopter toUpdate, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public PawsEntity.PetAdopter Find(object id, IDbConnection conn)
        {
            throw new NotImplementedException();
        }

        public List<PawsEntity.PetAdopter> FindAll(IDbConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
