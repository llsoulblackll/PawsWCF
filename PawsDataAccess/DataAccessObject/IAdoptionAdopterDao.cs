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
        List<AdoptionAdopter> FindAll(IDbConnection conn, int adoptionId = 0, int adopterId = 0);
        //HIDE PARENT DEFINITION OF THE SAME METHOD, USING THE NEW KEYWORD  
        new bool Insert(AdoptionAdopter toInsert, IDbConnection conn);
        int Count(IDbConnection conn, int ownerId = 0);
    }
}
