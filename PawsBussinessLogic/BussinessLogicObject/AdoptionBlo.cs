using PawsDataAccess.DataAccessObject;
using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace PawsBussinessLogic.BussinessLogicObject
{
    public class AdoptionBlo : IEntityBlo<Adoption>
    {
        IDbConnection conn;
        IAdoptionDao adoptionDao;

        public AdoptionBlo()
        {
            adoptionDao = DaoFactory.GetAdoptionDao();
        }

        public int Insert(Adoption toInsert)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionDao.Insert(toInsert, conn);
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

        public bool Update(Adoption toUpdate)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionDao.Update(toUpdate, conn);
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

        public bool Delete(object id)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionDao.Delete(id, conn);
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

        public Adoption Find(object id)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionDao.Find(id, conn);
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

        public List<Adoption> FindAll()
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionDao.FindAll(conn);
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
