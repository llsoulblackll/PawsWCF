using PawsDataAccess.DataAccessObject;
using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsBussinessLogic.BussinessLogicObject
{
    /// <summary>
    /// Do not instatiate directly, use factory instead, since class implementation is completely transparent, we do not expose an iterface but the class itself
    /// </summary>
    public class OwnerBlo : IEntityBlo<Owner>
    {
        private IOwnerDao ownerDao;
        private IDbConnection conn;

        public OwnerBlo()
        {
            ownerDao = DaoFactory.GetOwnerDao();
        }

        public int Insert(Owner toInsert)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return ownerDao.Insert(toInsert, conn);
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

        public bool Update(Owner toUpdate)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return ownerDao.Update(toUpdate, conn);
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
                    return ownerDao.Delete(id, conn);
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

        public Owner Find(object id)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return ownerDao.Find(id, conn);
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

        public List<Owner> FindAll()
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return ownerDao.FindAll(conn);
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

        public Owner Login(Owner owner)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return ownerDao.Login(owner, conn);
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
