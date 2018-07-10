using PawsDataAccess.DataAccessObject;
using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsBussinessLogic.BussinessLogicObject
{
    public class AuthBlo : IEntityBlo<Auth>
    {
        private IAuthDao authDao = DaoFactory.GetAuthDao();
        private IOwnerDao ownerDao = DaoFactory.GetOwnerDao();

        public int Insert(Auth toInsert)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return authDao.Insert(toInsert, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public bool Update(Auth toUpdate)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return authDao.Update(toUpdate, conn);
                }
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
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return authDao.Delete(id, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public Auth Find(object id)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return authDao.Find(id, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public List<Auth> FindAll()
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return authDao.FindAll(conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        //THIS SHOULD BE PART OF THE OWNER OBJECT
        public Auth Validate(Owner owner)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    string hash = GetMD5Hash($"{DateTime.Now.Millisecond}{owner.Id}{owner.Name}{owner.LastName}");

                    Auth auth = new Auth
                    {
                        CreatedAt = DateTime.Now,
                        Token = hash
                    };
                    
                    var o = ownerDao.Login(owner, conn);
                    if (o != null)
                    {
                        int genId = authDao.Insert(auth, conn);
                    }

                    return auth;
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public static string GetMD5Hash(string toCompute)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(toCompute));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
