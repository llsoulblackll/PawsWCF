using PawsEntity;
using System.Collections.Generic;
using System.Data;

namespace PawsDataAccess.DataAccessObject
{
    public interface IPetDao : IEntityDao<Pet>
    {
        List<Pet> FindAll(object ownerId, IDbConnection conn);
        int Count(IDbConnection conn, int ownerId = 0);
    }
}