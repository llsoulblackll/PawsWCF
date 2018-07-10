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
    public class AuthService : IAuthService
    {
        private AuthBlo authBlo = BloFactory.GetAuthBlo();
        
        public WCFResponse<object> New(AuthContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(AuthContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<AuthContract> Validate(OwnerContract owner)
        {
            return new WCFResponse<AuthContract>
            {

            };
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<AuthContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<AuthContract>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
