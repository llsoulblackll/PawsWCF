using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsBussinessLogic.DataTransferObject;
using PawsEntity;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;

namespace PawsWCF.Service
{
    public class PetAdopterService : IPetAdopterService
    {
        private PetAdopterBlo petAdopterBlo = BloFactory.GetPetAdopterBlo();

        public WCFResponse<object> New(PetAdopterContract toInsert)
        {
            //DEFAULT STATE
            toInsert.State = true;

            int result = petAdopterBlo.Insert(new PetAdopter
            {
                AdopterId = toInsert.AdopterId,
                PetId = toInsert.PetId,
                RequestDate = toInsert.RequestDate,
                //WE CAN OMIT THIS VALUE, IT IS OMMITED IN THE DATA LAYER AS WELL
                //ResponseDate = toInsert.ResponseDate,
                State = toInsert.State
            });

            if (result > 0)
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = result > 0
                };
            }
            else
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = result > 0
                };
            }
        }

        public WCFResponse<object> Update(PetAdopterContract toUpdate)
        {
            bool result = petAdopterBlo.Update(new PetAdopter
            {
                AdopterId = toUpdate.AdopterId,
                PetId = toUpdate.PetId,
                RequestDate = toUpdate.RequestDate,
                ResponseDate = toUpdate.ResponseDate,
                State = toUpdate.State
            });

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
            throw new NotImplementedException();
        }

        public WCFResponse<PetAdopterContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<PetAdopterContract> Find(string petId, string adopterId)
        {
            PetAdopter result = petAdopterBlo.Find(petId, adopterId);

            if(result != null)
            {
                return new WCFResponse<PetAdopterContract>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = new PetAdopterContract
                    {
                        AdopterId = result.AdopterId,
                        PetId = result.PetId,
                        RequestDate = result.RequestDate,
                        ResponseDate = result.ResponseDate,
                        State = result.State
                    }
                };
            }
            else
            {
                return new WCFResponse<PetAdopterContract>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<PetAdopterContract>> FindAll()
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<PetAdopterContract>> FindAllRequests(string ownerId)
        {
            List<PetAdopter> lPetAdopter = petAdopterBlo.FindAllRequests(ownerId);

            if(lPetAdopter != null)
            {
                return new WCFResponse<List<PetAdopterContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = lPetAdopter.Select(p =>
                    {
                        return new PetAdopterContract
                        {
                            AdopterId = p.AdopterId,
                            PetId = p.PetId,
                            RequestDate = p.RequestDate,
                            ResponseDate = p.ResponseDate,
                            State = p.State
                        };
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<PetAdopterContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<PetAdopterContract>> FindAllAnswers(string adopterId)
        {
            List<PetAdopter> lPetAdopter = petAdopterBlo.FindAllAnswers(adopterId);

            if (lPetAdopter != null)
            {
                return new WCFResponse<List<PetAdopterContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = lPetAdopter.Select(p =>
                    {
                        return new PetAdopterContract
                        {
                            AdopterId = p.AdopterId,
                            PetId = p.PetId,
                            RequestDate = p.RequestDate,
                            ResponseDate = p.ResponseDate,
                            State = p.State
                        };
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<PetAdopterContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<PetAdopterDtoContract>> FindAllRequestsDto(string ownerId)
        {
            List<PetAdopterDto> lPetAdopter = petAdopterBlo.FindAllRequestsDto(int.Parse(ownerId));

            if (lPetAdopter != null)
            {
                return new WCFResponse<List<PetAdopterDtoContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = lPetAdopter.Select(p =>
                    {
                        return new PetAdopterDtoContract
                        {
                            Adopter = p.Adopter,
                            Pet = p.Pet,
                            RequestDate = p.RequestDate,
                            ResponseDate = p.ResponseDate,
                            State = p.State
                        };
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<PetAdopterDtoContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<PetAdopterDtoContract>> FindAllAnswersDto(string adopterId)
        {
            List<PetAdopterDto> lPetAdopter = petAdopterBlo.FindAllAnswersDto(int.Parse(adopterId));

            if (lPetAdopter != null)
            {
                return new WCFResponse<List<PetAdopterDtoContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = lPetAdopter.Select(p =>
                    {
                        return new PetAdopterDtoContract
                        {
                            Adopter = p.Adopter,
                            Pet = p.Pet,
                            RequestDate = p.RequestDate,
                            ResponseDate = p.ResponseDate,
                            State = p.State
                        };
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<PetAdopterDtoContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }
    }
}
