using System.Collections.Generic;
using System.Data;

namespace PawsDataAccess.DataAccessObject
{
    public interface IEntityDao<T>
    {
        int Insert(T toInsert, IDbConnection conn);
        bool Update(T toUpdate, IDbConnection conn);
        bool Delete(object id, IDbConnection conn);
        T Find(object id, IDbConnection conn);
        List<T> FindAll(IDbConnection conn);
    }
}
