using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsDataAccess.DataAccessObject
{
    public interface IAdoptionPetDao : IEntityDao<AdoptionPet>
    {
        new bool Insert(AdoptionPet toInsert, IDbConnection conn);
        bool Delete(object adoptionId, object petId, IDbConnection conn);
        List<AdoptionPet> FindAll(object adoptionId, IDbConnection conn);
    }
}
