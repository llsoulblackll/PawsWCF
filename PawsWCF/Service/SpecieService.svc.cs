using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;

namespace PawsWCF.Service
{
    public class SpecieService : ISpecieService
    {
        SpecieBlo specieBlo;

        public SpecieService()
        {
            specieBlo = BloFactory.GetSpecieBlo();
        }

        public WCFResponse<object> New(SpecieContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(SpecieContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<SpecieContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<SpecieContract>> FindAll()
        {
            var species = specieBlo.FindAll();

            if(species != null)
            {
                return new WCFResponse<List<SpecieContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = species.Select(s => new SpecieContract
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<SpecieContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }
    }
}
