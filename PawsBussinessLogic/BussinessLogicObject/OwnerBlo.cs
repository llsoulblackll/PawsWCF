using PawsBussinessLogic.DataTransferObject;
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
        private IDistrictDao districtDao;
        private IPetDao petDao;
        private IDbConnection conn;

        public OwnerBlo()
        {
            ownerDao = DaoFactory.GetOwnerDao();
            districtDao = DaoFactory.GetDistrictDao();
            petDao = DaoFactory.GetPetDao();
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

        public OwnerDto FullLogin(Owner owner)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    owner = ownerDao.Login(owner, conn);
                    OwnerDto dto = new OwnerDto();
                    dto.Id = owner.Id;
                    dto.Username = owner.Username;
                    dto.Password = owner.Password;
                    dto.Name = owner.Name;
                    dto.LastName = owner.LastName;
                    dto.BirthDate = owner.BirthDate;
                    dto.DNI = owner.DNI;
                    dto.EMail = owner.EMail;
                    dto.Address = owner.Address;
                    dto.PhoneNumber = owner.PhoneNumber;
                    dto.ProfilePicture = owner.ProfilePicture;
                    //TEMPORARY, REPLACE LINQ FOR SP
                    //dto.District = districtDao.FindAll(conn).Where(d => d.Id == owner.DistrictId).FirstOrDefault();
                    dto.District = districtDao.Find(owner.Id, conn);
                    dto.Pets = petDao.FindAll(owner.Id, conn);

                    return dto;
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
