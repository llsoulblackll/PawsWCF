using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;
using PawsEntity;

namespace PawsWCF.Service
{
    public class DistrictService : IDistrictService
    {
        DistrictBlo districtBlo;

        public DistrictService()
        {
            districtBlo = BloFactory.GetDistrictBlo();
        }

        public WCFResponse<object> New(DistrictContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(DistrictContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<DistrictContract> Find(string id)
        {
            District dis = districtBlo.Find(int.Parse(id));

            if(dis != null)
            {
                return new WCFResponse<DistrictContract>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = new DistrictContract
                    {
                        Id = dis.Id,
                        Name = dis.Name
                    }
                };
            }
            else
            {
                return new WCFResponse<DistrictContract>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<DistrictContract>> FindAll()
        {
            var districts = districtBlo.FindAll();

            if(districts != null)
            {
                return new WCFResponse<List<DistrictContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = districts.Select(d => new DistrictContract
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<DistrictContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = null                };
            }
        }
    }
}
