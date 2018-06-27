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
        private IAdoptionAdopterDao adoptionAdopterDao;
        private IDbConnection conn;

        public OwnerBlo()
        {
            ownerDao = DaoFactory.GetOwnerDao();
            districtDao = DaoFactory.GetDistrictDao();
            petDao = DaoFactory.GetPetDao();
            adoptionAdopterDao = DaoFactory.GetAdoptionAdopterDao();
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
                    OwnerDto ownerDto = new OwnerDto();
                    ownerDto.Id = owner.Id;
                    ownerDto.Username = owner.Username;
                    ownerDto.Password = owner.Password;
                    ownerDto.Name = owner.Name;
                    ownerDto.LastName = owner.LastName;
                    ownerDto.BirthDate = owner.BirthDate;
                    ownerDto.DNI = owner.DNI;
                    ownerDto.EMail = owner.EMail;
                    ownerDto.Address = owner.Address;
                    ownerDto.PhoneNumber = owner.PhoneNumber;
                    ownerDto.ProfilePicture = owner.ProfilePicture;
                    //TEMPORARY, REPLACE LINQ FOR SP
                    //dto.District = districtDao.FindAll(conn).Where(d => d.Id == owner.DistrictId).FirstOrDefault();
                    ownerDto.District = districtDao.Find(ownerDto.Id, conn);
                    ownerDto.RegisteredAmount = petDao.Count(conn, ownerDto.Id);
                    ownerDto.AdoptedAmount = 0;//adoptionAdopterDao.Count(conn, ownerDto.Id);
                    //dto.Pets = petDao.FindAll(owner.Id, conn);

                    return ownerDto;
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
