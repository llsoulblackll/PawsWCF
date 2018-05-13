using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsWCF.Contract;

namespace PawsWCF.Service
{
    public class FoundPetService : IFoundPetService
    {
        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<FoundPetContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<FoundPetContract>> FindAll()
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> New(FoundPetContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(FoundPetContract toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
