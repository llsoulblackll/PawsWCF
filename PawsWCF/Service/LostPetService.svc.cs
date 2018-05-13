using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsWCF.Contract;

namespace PawsWCF.Service
{
    public class LostPetService : ILostPetService
    {
        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<LostPetContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<LostPetContract>> FindAll()
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> New(LostPetContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(LostPetContract toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
