using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsEntity;
using PawsWCF.Contract;
using static PawsWCF.WCFConstant.Constant;
using static PawsWCF.WCFConstant.WCFResponseCode;
using static PawsWCF.WCFConstant.WCFResponseMessage;

namespace PawsWCF.Service
{
    public class AdoptionPetService : IAdoptionPetService
    {
        private AdoptionPetBlo adoptionPetBlo;

        public AdoptionPetService()
        {
            adoptionPetBlo = BloFactory.GetAdoptionPetBlo();
        }

        public WCFResponse<bool> New(AdoptionPetContract adoptionPet)
        {
            var res = adoptionPetBlo.Insert(new AdoptionPet
            {
                AdoptionId = adoptionPet.AdoptionId,
                PetId = adoptionPet.PetId
            });

            if (res)
            {
                return new WCFResponse<bool>
                {
                    ResponseCode = Success,
                    ResponseMessage = WCF_SUCCESS,
                    Response = res
                };
            }

            else
            {
                return new WCFResponse<bool>
                {
                    ResponseCode = Error,
                    ResponseMessage = WCF_ERROR,
                    Response = res
                };
            }
        }

        WCFResponse<object> IEntityService<AdoptionPetContract>.New(AdoptionPetContract toInsert)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Update(AdoptionPetContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<bool> Delete(string adoptionId, string petId)
        {
            var res = adoptionPetBlo.Delete(adoptionId, petId);

            if (res)
            {
                return new WCFResponse<bool>
                {
                    ResponseCode = Success,
                    ResponseMessage = WCF_SUCCESS,
                    Response = res
                };
            }
            else
            {
                return new WCFResponse<bool>
                {
                    ResponseCode = Error,
                    ResponseMessage = WCF_ERROR,
                    Response = res
                };
            }
        }

        public WCFResponse<AdoptionPetContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<AdoptionPetContract>> FindAll()
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<AdoptionPetContract>> FindAll(object adoptionId)
        {
            var res = adoptionPetBlo.FindAll(adoptionId).Select(a => new AdoptionPetContract
            {
                AdoptionId = a.AdoptionId,
                PetId = a.PetId
            }).ToList();

            if (res != null)
            {
                return new WCFResponse<List<AdoptionPetContract>>
                {
                    ResponseCode = Success,
                    ResponseMessage = WCF_SUCCESS,
                    Response = res
                };
            }
            else
            {
                return new WCFResponse<List<AdoptionPetContract>>
                {
                    ResponseCode = Error,
                    ResponseMessage = WCF_ERROR,
                    Response = res
                };
            }
        }
    }
}
