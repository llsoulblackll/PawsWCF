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
    public class RaceService : IRaceService
    {
        RaceBlo raceBlo;

        public RaceService()
        {
            raceBlo = BloFactory.GetRaceBlo();
        }

        public WCFResponse<object> New(RaceContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(RaceContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<RaceContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<RaceContract>> FindAll()
        {
                throw new NotImplementedException();
        }

        public WCFResponse<List<RaceContract>> FindAll(string specieId)
        {
            var races = raceBlo.FindAll(int.Parse(specieId));

            if(races != null)
            {
                return new WCFResponse<List<RaceContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = races.Select(r => new RaceContract
                    {
                        Id = r.Id,
                        Name = r.Name,
                        SpecieId = r.SpecieId
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<RaceContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }
    }
}
