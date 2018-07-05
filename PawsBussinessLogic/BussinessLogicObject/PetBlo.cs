using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawsBussinessLogic.DataTransferObject;
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
        private ISpecieDao specieDao;
        private IRaceDao raceDao;
        private IOwnerDao ownerDao;

        public PetBlo()
        {
            petDao = DaoFactory.GetPetDao();
            specieDao = DaoFactory.GetSpecieDao();
            raceDao = DaoFactory.GetRaceDao();
            ownerDao = DaoFactory.GetOwnerDao();
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

        public List<PetDto> FindAllDto()
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.FindAll(conn).Select(p =>
                    {
                        return new PetDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Age = p.Age,
                            Description = p.Description,
                            Picture = p.Picture,
                            PublishDate = p.PublishDate,
                            State = p.State,
                            OtherRace = p.OtherRace,
                            Specie = specieDao.Find(p.SpecieId, conn),
                            Race = raceDao.Find(p.RaceId, conn),
                            Owner = ownerDao.Find(p.OwnerId, conn)
                        };
                    }).ToList();
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

        public List<PetDto> FindAllDto(object ownerId)
        {
            try
            {
                using (var conn = ConnectionFactory.GetOpenConnection())
                {
                    return petDao.FindAll(ownerId, conn).Select(p =>
                    {
                        return new PetDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Age = p.Age,
                            Description = p.Description,
                            Picture = p.Picture,
                            PublishDate = p.PublishDate,
                            State = p.State,
                            OtherRace = p.OtherRace,
                            Specie = specieDao.Find(p.SpecieId, conn),
                            Race = raceDao.Find(p.RaceId, conn),
                            Owner = ownerDao.Find(p.OwnerId, conn)
                        };
                    }).ToList();
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
