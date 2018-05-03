using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PawsBussinessLogic.BussinessLogicObject;
using PawsEntity;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;

namespace PawsWCF.Service
{
    public class PetService : IPetService
    {
        private PetBlo petBlo;

        public PetService()
        {
            this.petBlo = new PetBlo();
        }

        public WCFResponse<object> NewPet(PetContract pet)
        {
            var petEntity = new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Description = pet.Description,
                Picture = pet.Picture,
                RaceId = pet.RaceId,
                OwnerId = pet.OwnerId

            };

            int genId = petBlo.Insert(petEntity);
            string partialPath = $"{genId}_{pet.Name}_{DateTime.Now.Ticks}.{pet.ImageExtension}";
            string savePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Uploads\\{partialPath}"));
            File.WriteAllBytes(savePath, Convert.FromBase64String(pet.ImageBase64));

            petEntity.Id = genId;
            petEntity.Picture = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/Uploads/{partialPath}";

            bool result;
            string message;
            WCFResponseCode responseCode;
            if (File.Exists(savePath))
                result = petBlo.Update(petEntity);
            else
            {
                petBlo.Delete(pet.Id);
                result = false;
            }

            if (result)
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = result.ToString()
                };
            }
            else
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = result.ToString()
                };
            }
        }

        public WCFResponse<object> UpdatePet(PetContract pet)
        {
            var petEntity = new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Description = pet.Description,
                Picture = pet.Picture,
                RaceId = pet.RaceId,
                OwnerId = pet.OwnerId
            };

            var result = petBlo.Update(petEntity);
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

        public WCFResponse<object> DeletePet(string id)
        {
            var response = petBlo.Delete(int.Parse(id));
            if (response)
            {
                return new WCFResponse<object>
                {
                    Response = response.ToString(),
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS
                };
            }
            else
            {
                return new WCFResponse<object>
                {
                    Response = response.ToString(),
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR
                };
            }
        }

        public WCFResponse<PetContract> FindPet(string id)
        {
            var petEntity = petBlo.Find(int.Parse(id));
            //OMIT IMAGEBASE64 AND IMAGEEXTENSION SINCE WE ONLY NEED THOSE WHEN INSERTING

            if (petEntity != null)
            {
                var pet = new PetContract
                {
                    Id = petEntity.Id,
                    Name = petEntity.Name,
                    Age = petEntity.Age,
                    Description = petEntity.Description,
                    Picture = petEntity.Picture,
                    RaceId = petEntity.RaceId,
                    OwnerId = petEntity.OwnerId
                };

                return new WCFResponse<PetContract>
                {
                    Response = pet,
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS
                };
            }
            else
            {
                return new WCFResponse<PetContract>
                {
                    Response = null,
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR
                };
            }
        }

        public WCFResponse<List<PetContract>> FindAllPets()
        {
            var pets = petBlo.FindAll();
            if (pets != null) {
                return new WCFResponse<List<PetContract>>
                {
                    Response = pets.Select(p => new PetContract
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Age = p.Age,
                        Description = p.Description,
                        Picture = p.Picture,
                        RaceId = p.RaceId,
                        OwnerId = p.OwnerId
                    }).ToList(),
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS
                };
            }
            else
            {
                return new WCFResponse<List<PetContract>>
                {
                    Response = null,
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR
                };
            }
        }
    }
}
