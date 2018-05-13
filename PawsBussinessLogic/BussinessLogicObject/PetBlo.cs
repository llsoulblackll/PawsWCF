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
    /// <summary>
    /// Do not instatiate directly, use factory instead
    /// </summary>
    public class PetBlo : IEntityBlo<Pet>
    {
        private IPetDao petDao;

        public PetBlo()
        {
            petDao = DaoFactory.GetPetDao();
        }

        public int Insert(Pet toInsert)
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.Insert(toInsert, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
        }

        public bool Update(Pet toUpdate)
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.Update(toUpdate, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
        }
        public bool Delete(object id)
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.Delete(id, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
        }

        public Pet Find(object id)
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.Find(id, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
        }

        public List<Pet> FindAll()
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.FindAll(conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
        }

        public List<Pet> FindAll(object ownerId)
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.FindAll(ownerId, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
            catch (DbException ex)
            {
                throw;
            }
        }
    }
}
