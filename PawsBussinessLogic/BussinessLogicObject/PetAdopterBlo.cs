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
    public class PetAdopterBlo : IEntityBlo<PetAdopter>
    {
        private IPetAdopterDao petAdopterDao = DaoFactory.GetPetAdopterDao();
        private IOwnerDao ownerDao = DaoFactory.GetOwnerDao();
        private IPetDao petDao = DaoFactory.GetPetDao();

        public int Insert(PetAdopter toInsert)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.Insert(toInsert, conn);
                }
            }
            catch (DataException ex)
            {
                //TODO: REMOVE THIS AND CHECK IF RECORDS EXISTS IN THE TABLE WITH THE SP
                return -1;
            }
        }

        public bool Update(PetAdopter toUpdate)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.Update(toUpdate, conn);
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
                    return petAdopterDao.Delete(id, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public PetAdopter Find(object id)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.Find(id, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public PetAdopter Find(object petId, object adopterId)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.Find(petId, adopterId, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public List<PetAdopter> FindAll()
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.FindAll(conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public List<PetAdopter> FindAllRequests(object ownerId)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.FindAllRequests(ownerId, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public List<PetAdopter> FindAllAnswers(object adopterId)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.FindAllRequests(adopterId, conn);
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public List<PetAdopterDto> FindAllRequestsDto(object ownerId)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.FindAllRequests(ownerId, conn).Select(pa =>
                    {
                        Pet p = petDao.Find(pa.PetId, conn);
                        return new PetAdopterDto
                        {
                            //RACE AND SPECIE ARE NOT NEEDED FOR THIS LIST
                            Adopter = ownerDao.Find(pa.AdopterId, conn),
                            Pet = new PetDto
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Age = p.Age,
                                Description = p.Description,
                                Picture = p.Picture,
                                PublishDate = p.PublishDate,
                                State = p.State,
                                OtherRace = p.OtherRace,
                                Specie = null,
                                Race = null,
                                Owner = ownerDao.Find(p.OwnerId, conn)
                            },
                            RequestDate = pa.RequestDate,
                            ResponseDate = pa.ResponseDate,
                            State = pa.State
                        };
                    }).ToList();
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }

        public List<PetAdopterDto> FindAllAnswersDto(object adopterId)
        {
            try
            {
                using (IDbConnection conn = ConnectionFactory.GetOpenConnection())
                {
                    return petAdopterDao.FindAllAnswers(adopterId, conn).Select(pa =>
                    {
                        Pet p = petDao.Find(pa.PetId, conn);
                        return new PetAdopterDto
                        {
                            Adopter = ownerDao.Find(pa.AdopterId, conn),
                            Pet = new PetDto
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Age = p.Age,
                                Description = p.Description,
                                Picture = p.Picture,
                                PublishDate = p.PublishDate,
                                State = p.State,
                                OtherRace = p.OtherRace,
                                Specie = null,
                                Race = null,
                                Owner = ownerDao.Find(p.OwnerId, conn)
                            },
                            RequestDate = pa.RequestDate,
                            ResponseDate = pa.ResponseDate,
                            State = pa.State
                        };
                    }).ToList();
                }
            }
            catch (DataException ex)
            {
                throw;
            }
        }
    }
}
