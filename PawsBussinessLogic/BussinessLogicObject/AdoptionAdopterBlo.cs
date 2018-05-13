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
    public class AdoptionAdopterBlo : IEntityBlo<AdoptionAdopter>
    {
        IDbConnection conn;
        IAdoptionAdopterDao adoptionAdopterDao;

        public AdoptionAdopterBlo()
        {
            adoptionAdopterDao = DaoFactory.GetAdoptionAdopterDao();
        }

        public int Insert(AdoptionAdopter toInsert)
        {
            throw new NotImplementedException("This insertion does not return any id, use boolean insert instead");
        }

        public bool InsertAdoptionAdopter(AdoptionAdopter toInsert)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionAdopterDao.Insert(toInsert, conn);
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

        public bool Update(AdoptionAdopter toUpdate)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionAdopterDao.Update(toUpdate, conn);
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
                    return adoptionAdopterDao.Delete(id, conn);
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

        public AdoptionAdopter Find(object id)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionAdopterDao.Find(id, conn);
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

        public List<AdoptionAdopter> FindAll()
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionAdopterDao.FindAll(conn);
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

        public List<AdoptionAdopter> FindAll(object adoptionId)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return adoptionAdopterDao.FindAll(adoptionId, conn);
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
