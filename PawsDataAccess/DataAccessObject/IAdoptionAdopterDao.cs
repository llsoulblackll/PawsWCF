using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;

namespace PawsDataAccess.DataAccessObject
{
    public interface IAdoptionAdopterDao : IEntityDao<AdoptionAdopter>
    {
        bool Delete(object adoptionId, object adopterId, IDbConnection conn);
        AdoptionAdopter Find(object adoptionId, object adopterId, IDbConnection conn);
        List<AdoptionAdopter> FindAll(object adoptionId, IDbConnection conn);
        //HIDE PARENT DEFINITION OF THE SAME METHOD, USING THE NEW KEYWORD  
        new bool Insert(AdoptionAdopter toInsert, IDbConnection conn);
    }
}
