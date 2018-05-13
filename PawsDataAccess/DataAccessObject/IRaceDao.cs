using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsDataAccess.DataAccessObject
{
    public interface IRaceDao : IEntityDao<Race>
    {
        List<Race> FindAll(object specieId, IDbConnection conn);
    }
}
