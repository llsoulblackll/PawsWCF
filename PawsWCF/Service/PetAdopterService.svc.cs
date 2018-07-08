using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsWCF.Contract;

namespace PawsWCF.Service
{
    public class PetAdopterService : IPetAdopterService
    {
        private PetAdopterBlo perAdopterBlo = BloFactory.GetPetAdopterBlo();

        public WCFResponse<object> New(PetAdopterContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(PetAdopterContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<PetAdopterContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<PetAdopterContract>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
