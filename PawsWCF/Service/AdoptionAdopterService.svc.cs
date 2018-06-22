using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsEntity;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;

namespace PawsWCF.Service
{
    public class AdoptionAdopterService : IAdoptionAdopterService
    {
        AdoptionAdopterBlo adoptionAdopterBlo;

        public AdoptionAdopterService()
        {
            adoptionAdopterBlo = BloFactory.GetAdoptionAdopterBlo();
        }

        public WCFResponse<object> New(AdoptionAdopterContract toInsert)
        {
            var result = adoptionAdopterBlo.Insert(new AdoptionAdopter
            {
                AdoptionId = toInsert.AdoptionId,
                AdopterId = toInsert.AdopterId,
                AdoptedQuantity = toInsert.AdoptedQuantity,
                AdoptedDate = toInsert.AdoptedDate
            });

            return new WCFResponse<object>
            {
                ResponseCode = WCFResponseCode.Success,
                ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                Response = result
            };
        }

        public WCFResponse<object> Update(AdoptionAdopterContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            var result = adoptionAdopterBlo.Delete(int.Parse(id));

            if (result)
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = result
                };
            }
            else
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = result
                };
            }
        }

        public WCFResponse<AdoptionAdopterContract> Find(string id)
        {
            var adoptionAdopterEntity = adoptionAdopterBlo.Find(int.Parse(id));

            if (adoptionAdopterEntity != null)
            {
                return new WCFResponse<AdoptionAdopterContract>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = new AdoptionAdopterContract
                    {
                        AdoptionId = adoptionAdopterEntity.AdoptionId,
                        AdopterId = adoptionAdopterEntity.AdopterId,
                        AdoptedQuantity =  adoptionAdopterEntity.AdoptedQuantity,
                        AdoptedDate = adoptionAdopterEntity.AdoptedDate
                    }
                };
            }
            else
            {
                return new WCFResponse<AdoptionAdopterContract>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<AdoptionAdopterContract>> FindAll()
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<AdoptionAdopterContract>> FindAll(string adoptionId)
        {
            var adoptionAdopters = adoptionAdopterBlo.FindAll(int.Parse(adoptionId));

            if (adoptionAdopters != null)
            {
                return new WCFResponse<List<AdoptionAdopterContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = adoptionAdopters.Select(a => new AdoptionAdopterContract
                    {
                        AdoptionId = a.AdoptionId,
                        AdopterId = a.AdopterId,
                        AdoptedQuantity = a.AdoptedQuantity,
                        AdoptedDate = a.AdoptedDate
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<AdoptionAdopterContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }
    }
}
