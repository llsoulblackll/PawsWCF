using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsDataAccess.DataAccessObject;
using PawsEntity;

namespace PawsBussinessLogic.BussinessLogicObject
{
    public class AdoptionPetBlo : IEntityBlo<AdoptionPet>
    {
        IDbConnection conn;
        IAdoptionPetDao adoptionPetDao;

        public AdoptionPetBlo()
        {
            adoptionPetDao = DaoFactory.GetAdoptionPetDao();
        }

        int IEntityBlo<AdoptionPet>.Insert(AdoptionPet toInsert)
        {
            throw new NotImplementedException();
        }

        public bool Insert(AdoptionPet toInsert)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionPetDao.Insert(toInsert, conn);
                }
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public bool Update(AdoptionPet toUpdate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object adoptionId, object petId)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionPetDao.Delete(adoptionId, petId, conn);
                }
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public AdoptionPet Find(object id)
        {
            throw new NotImplementedException();
        }

        public List<AdoptionPet> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<AdoptionPet> FindAll(object adoptionId)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionPetDao.FindAll(adoptionId, conn);
                }
            }
            catch (DbException ex)
            {
                throw;
            }
            catch (DataException ex)
            {
                throw;
            }
        }
    }
}
