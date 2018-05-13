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
    public class AdoptionService : IAdoptionService
    {
        AdoptionBlo adoptionBlo;

        public AdoptionService()
        {
            adoptionBlo = BloFactory.GetAdoptionBlo();
        }

        public WCFResponse<object> New(AdoptionContract toInsert)
        {
            var adoptionEntity = new Adoption
            {
                Id = toInsert.Id,
                State = toInsert.State,
                Description = toInsert.Description,
                Address = toInsert.Address,
                Age = toInsert.Age,
                TotalQuantity = toInsert.TotalQuantity,
                AvailableQuantity = toInsert.AvailableQuantity,
                PublishDate = toInsert.PublishDate,
                DistrictId = toInsert.DistrictId,
                OwnerId = toInsert.OwnerId,
                SpecieId = toInsert.SpecieId,
                RaceId = toInsert.RaceId,
                PetId = toInsert.PetId
            };

            return new WCFResponse<object>
            {
                ResponseCode = WCFResponseCode.Success,
                ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                Response = adoptionBlo.Insert(adoptionEntity)
            };
        }

        public WCFResponse<object> Update(AdoptionContract toUpdate)
        {
            var adoptionEntity = new Adoption
            {
                Id = toUpdate.Id,
                State = toUpdate.State,
                Description = toUpdate.Description,
                Address = toUpdate.Address,
                Age = toUpdate.Age,
                TotalQuantity = toUpdate.TotalQuantity,
                AvailableQuantity = toUpdate.AvailableQuantity,
                PublishDate = toUpdate.PublishDate,
                DistrictId = toUpdate.DistrictId,
                OwnerId = toUpdate.OwnerId,
                RaceId = toUpdate.RaceId,
                PetId = toUpdate.PetId
            };

            var result = adoptionBlo.Update(adoptionEntity);

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

        public WCFResponse<object> Delete(string id)
        {
            var result = adoptionBlo.Delete(int.Parse(id));

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

        public WCFResponse<AdoptionContract> Find(string id)
        {
            var adoptionEntity = adoptionBlo.Find(int.Parse(id));

            if (adoptionEntity != null)
            {
                return new WCFResponse<AdoptionContract>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = new AdoptionContract
                    {
                        Id = adoptionEntity.Id,
                        State = adoptionEntity.State,
                        Description = adoptionEntity.Description,
                        Address = adoptionEntity.Address,
                        Age = adoptionEntity.Age,
                        TotalQuantity = adoptionEntity.TotalQuantity,
                        AvailableQuantity = adoptionEntity.AvailableQuantity,
                        PublishDate = adoptionEntity.PublishDate,
                        DistrictId = adoptionEntity.DistrictId,
                        OwnerId = adoptionEntity.OwnerId,
                        SpecieId = adoptionEntity.SpecieId,
                        RaceId = adoptionEntity.RaceId,
                        PetId = adoptionEntity.PetId
                    }
                };
            }
            else
            {
                return new WCFResponse<AdoptionContract>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<AdoptionContract>> FindAll()
        {
            var adoptions = adoptionBlo.FindAll();

            if (adoptions != null)
            {
                return new WCFResponse<List<AdoptionContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = adoptions.Select(a => new AdoptionContract
                    {
                        Id = a.Id,
                        State = a.State,
                        Description = a.Description,
                        Address = a.Address,
                        Age = a.Age,
                        TotalQuantity = a.TotalQuantity,
                        AvailableQuantity = a.AvailableQuantity,
                        PublishDate = a.PublishDate,
                        DistrictId = a.DistrictId,
                        OwnerId = a.OwnerId,
                        SpecieId = a.SpecieId,
                        RaceId = a.RaceId,
                        PetId = a.PetId
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<AdoptionContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }
    }
}
