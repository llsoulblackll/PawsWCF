using PawsDataAccess.Database;
using PawsEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsDataAccess.DataAccessObject
{
    public interface IOwnerDao : IEntityDao<Owner>
    {
        /// <summary>
        /// Login with username and password
        /// </summary>
        /// <param name="owner">Owner entity with only username and password</param>
        /// <returns>The complete owner entity</returns>
        Owner Login(Owner owner, IDbConnection conn);
    }
}
