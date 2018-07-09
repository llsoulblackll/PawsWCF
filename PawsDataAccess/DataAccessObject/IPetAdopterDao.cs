using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsDataAccess.DataAccessObject
{
    public interface IPetAdopterDao : IEntityDao<PetAdopter>
    {
        PetAdopter Find(object petId, object adopterId, IDbConnection conn);
        List<PetAdopter> FindAllRequests(object adopterId, IDbConnection conn);
        List<PetAdopter> FindAllAnswers(object adopterId, IDbConnection conn);
    }
}
