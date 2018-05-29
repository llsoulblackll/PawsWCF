using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using PawsDataAccess.DataAccessObject;
using PawsEntity;

namespace PawsBussinessLogic.BussinessLogicObject
{
    public class SurveyBlo : IEntityBlo<Survey>
    {
        private IDbConnection conn;
        private ISurveyDao surveyDao;

        public SurveyBlo()
        {
            surveyDao = DaoFactory.GetSurveyDao();
        }

        public int Insert(Survey toInsert)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return surveyDao.Insert(toInsert, conn);
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

        public bool Update(Survey toUpdate)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return surveyDao.Update(toUpdate, conn);
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
            throw new NotImplementedException();
        }

        public Survey Find(object id)
        {
            try
            {
                using (conn = ConnectionFactory.GetOpenConnection())
                {
                    return surveyDao.Find(id, conn);
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

        public List<Survey> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
