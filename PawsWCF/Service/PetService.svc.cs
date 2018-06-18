using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PawsBussinessLogic.BussinessLogicObject;
using PawsEntity;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;
using static PawsWCF.WCFConstant.Constant;
using static PawsWCF.Util.Util;

namespace PawsWCF.Service
{
    public class PetService : IPetService
    {
        private PetBlo petBlo;

        public PetService()
        {
            this.petBlo = BloFactory.GetPetBlo();
        }

        public WCFResponse<object> New(PetContract pet)
        {
            var petEntity = new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Description = pet.Description,
                Picture = pet.Picture,
                SpecieId = pet.SpecieId,
                RaceId = pet.RaceId,
                OwnerId = pet.OwnerId

            };

            int genId = petBlo.Insert(petEntity);
            petEntity.Id = genId;

            //bool result = genId > 0;

            if(!string.IsNullOrWhiteSpace(pet.ImageBase64) && !string.IsNullOrWhiteSpace(pet.ImageExtension))
            {
                string partialPath = $"{genId}_{pet.Name}_{DateTime.Now.Ticks}.{pet.ImageExtension}";
                bool result = IOUtil.SaveFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{UPLOAD_FOLDER}\\{partialPath}")), pet.ImageBase64);
                petEntity.Picture = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{partialPath}";

                if (result)
                    petBlo.Update(petEntity);
            }

            if (genId > 0)
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = genId
                };
            }
            else
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = genId
                };
            }
        }

        public WCFResponse<object> Update(PetContract pet)
        {
            var petEntity = new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Description = pet.Description,
                Picture = pet.Picture,
                SpecieId = pet.SpecieId,
                RaceId = pet.RaceId,
                OwnerId = pet.OwnerId
            };

            bool result = false;

            if (!string.IsNullOrWhiteSpace(pet.ImageBase64) && !string.IsNullOrWhiteSpace(pet.ImageExtension))
            {
                string partialPath = $"{petEntity.Id}_{pet.Name}_{DateTime.Now.Ticks}.{pet.ImageExtension}";
                result = IOUtil.SaveFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{UPLOAD_FOLDER}\\{partialPath}")), pet.ImageBase64);
                petEntity.Picture = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{partialPath}";

                if (result && string.IsNullOrWhiteSpace(petEntity.Picture))
                    IOUtil.DeleteFile(petEntity.Picture);
            }

            result = petBlo.Update(petEntity);

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
            int petId = int.Parse(id);
            Pet pet = petBlo.Find(petId);

            IOUtil.DeleteFile(pet.Picture);

            var response = petBlo.Delete(petId);
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

        public WCFResponse<PetContract> Find(string id)
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
                    SpecieId = petEntity.SpecieId,
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

        public WCFResponse<List<PetContract>> FindAll()
        {
            return new WCFResponse<List<PetContract>>
            {
                Response = petBlo.FindAll().Select(p => new PetContract
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Description = p.Description,
                    Picture = p.Picture,
                    SpecieId = p.SpecieId,
                    RaceId = p.RaceId,
                    OwnerId = p.OwnerId
                }).ToList(),
                ResponseCode = WCFResponseCode.Error,
                ResponseMessage = WCFResponseMessage.WCF_ERROR
            };
        }

        public WCFResponse<List<PetContract>> FindAll(string ownerId)
        {
            int id = int.Parse(ownerId);
            var pets = petBlo.FindAll(id);

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
                        SpecieId = p.SpecieId,
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




//string savePath = "";

//if (!string.IsNullOrWhiteSpace(pet.ImageBase64) && !string.IsNullOrWhiteSpace(pet.ImageExtension))
//{
//    string partialPath = $"{genId}_{pet.Name}_{DateTime.Now.Ticks}.{pet.ImageExtension}";
//    savePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{UPLOAD_FOLDER}\\{partialPath}"));
//    File.WriteAllBytes(savePath, Convert.FromBase64String(pet.ImageBase64));
//    petEntity.Picture = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{partialPath}";
//}

//bool result = true;//BY DEFAULT SINCE THE INSERTION WAS 'SUCCESSFUL'

//if (File.Exists(savePath))
//    result = petBlo.Update(petEntity);

///*else
//{
//    petBlo.Delete(pet.Id);
//    result = false;
//}*/

//if(!string.IsNullOrWhiteSpace(pet.ImageBase64) && !string.IsNullOrWhiteSpace(pet.ImageExtension))
//{
//    string fileName = $"{pet.Id}_{pet.Name}_{DateTime.Now.Ticks}.{pet.ImageExtension}";
//    string fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\${UPLOAD_FOLDER}\\{fileName}";
//    string serverPath = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{fileName}";
//    File.WriteAllBytes(fullPath, Convert.FromBase64String(pet.ImageBase64));

//    //DO IT AFTER THE WRITE TO ENSURE THE NEW IMAGE HAS BEEN SAVED
//    if (!string.IsNullOrWhiteSpace(pet.Picture))
//    {
//        File.Delete(pet.Picture);
//    }

//    petEntity.Picture = serverPath;
//}