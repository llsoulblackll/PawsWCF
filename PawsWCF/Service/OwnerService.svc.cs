using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using PawsBussinessLogic.BussinessLogicObject;
using PawsEntity;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;
using static PawsWCF.Util.Util;
using static PawsWCF.WCFConstant.Constant;

namespace PawsWCF.Service
{
    public class OwnerService : IOwnerService
    {
        OwnerBlo ownerBlo;

        public OwnerService()
        {
            ownerBlo = BloFactory.GetOwnerBlo();
        }

        public WCFResponse<object> New(OwnerContract toInsert)
        {
            var ownerEntity = new Owner
            {
                Id = toInsert.Id,
                Username = toInsert.Username,
                Password = toInsert.Password,
                Name = toInsert.Name,
                LastName = toInsert.LastName,
                BirthDate = toInsert.BirthDate,
                DNI = toInsert.DNI,
                EMail = toInsert.EMail,
                Address = toInsert.Address,
                PhoneNumber = toInsert.PhoneNumber,
                ProfilePicture = toInsert.ProfilePicture,
                DistrictId = toInsert.DistrictId
            };

            int genId = ownerBlo.Insert(ownerEntity);
            ownerEntity.Id = genId;

            bool result = genId > 0;

            //if(!string.IsNullOrWhiteSpace(toInsert.ImageBase64) && !string.IsNullOrWhiteSpace(toInsert.ImageExtension))
            //{
            //    string filePath = $"{genId}_{toInsert.Name}_{toInsert.LastName}_{DateTime.Now.Ticks}.{toInsert.ImageExtension}";
            //    result = IOUtil.SaveFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{UPLOAD_FOLDER}\\{filePath}", toInsert.ImageBase64);
            //    string serverPath = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{filePath}";
            //    ownerEntity.ProfilePicture = serverPath;

            //    if (result)
            //    {
            //        result = ownerBlo.Update(ownerEntity);
            //    }
            //}

            if (!string.IsNullOrWhiteSpace(toInsert.ImageBase64) && !string.IsNullOrWhiteSpace(toInsert.ImageExtension))
            {
                string url = AWSUtil.UploadToS3($"{genId}_{toInsert.Name}_{toInsert.LastName}_{DateTime.Now.Ticks}.{toInsert.ImageExtension}",
                    Convert.FromBase64String(toInsert.ImageBase64));

                ownerEntity.ProfilePicture = url;
                result = ownerBlo.Update(ownerEntity);
            }

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

        public WCFResponse<object> Update(OwnerContract toUpdate)
        {
            var ownerEntity = new Owner
            {
                Id = toUpdate.Id,
                Username = toUpdate.Username,
                Password = toUpdate.Password,
                Name = toUpdate.Name,
                LastName = toUpdate.LastName,
                BirthDate = toUpdate.BirthDate,
                DNI = toUpdate.DNI,
                EMail = toUpdate.EMail,
                Address = toUpdate.Address,
                PhoneNumber = toUpdate.PhoneNumber,
                ProfilePicture = toUpdate.ProfilePicture,
                DistrictId = toUpdate.DistrictId
            };

            bool result = false;

            //if (!string.IsNullOrWhiteSpace(toUpdate.ImageBase64) && !string.IsNullOrWhiteSpace(toUpdate.ImageExtension))
            //{
            //    string fileName = $"{ownerEntity.Id}_{ownerEntity.Name}_{ownerEntity.LastName}_{DateTime.Now.Ticks}.{toUpdate.ImageExtension}";
            //    result = IOUtil.SaveFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{UPLOAD_FOLDER}\\{fileName}", toUpdate.ImageBase64);
            //    string serverPath = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{fileName}";

            //    if (string.IsNullOrWhiteSpace(ownerEntity.ProfilePicture))
            //        IOUtil.DeleteFile(ownerEntity.ProfilePicture);

            //    ownerEntity.ProfilePicture = serverPath;
            //}

            if (!string.IsNullOrWhiteSpace(toUpdate.ImageBase64) && !string.IsNullOrWhiteSpace(toUpdate.ImageExtension))
            {
                string objectKey = $"{toUpdate.Id}_{toUpdate.Name}_{toUpdate.LastName}_{DateTime.Now.Ticks}.{toUpdate.ImageExtension}";
                string url = AWSUtil.UploadToS3(objectKey, Convert.FromBase64String(toUpdate.ImageBase64));

                if (!string.IsNullOrWhiteSpace(ownerEntity.ProfilePicture))
                    AWSUtil.DeleteFromS3(ownerEntity.ProfilePicture.Substring(ownerEntity.ProfilePicture.LastIndexOf('/') + 1));
                ownerEntity.ProfilePicture = url;
            }

            //UPDATE AFTER
            result = ownerBlo.Update(ownerEntity);

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
            int ownerId = int.Parse(id);
            Owner owner = ownerBlo.Find(ownerId);

            bool result = ownerBlo.Delete(ownerId); 

            if (result)
                result = AWSUtil.DeleteFromS3(owner.ProfilePicture.Substring(owner.ProfilePicture.LastIndexOf('/') + 1));

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

        public WCFResponse<OwnerContract> Find(string id)
        {
            var owner = ownerBlo.Find(int.Parse(id));

            if (owner != null)
            {
                return new WCFResponse<OwnerContract>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = new OwnerContract
                    {
                        Id = owner.Id,
                        Username = owner.Username,
                        Password = owner.Password,
                        Name = owner.Name,
                        LastName = owner.LastName,
                        BirthDate = owner.BirthDate,
                        DNI = owner.DNI,
                        EMail = owner.EMail,
                        Address = owner.Address,
                        PhoneNumber = owner.PhoneNumber,
                        ProfilePicture = owner.ProfilePicture,
                        DistrictId = owner.DistrictId
                    }
                };
            }
            else
            {
                return new WCFResponse<OwnerContract>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<List<OwnerContract>> FindAll()
        {
            var owners = ownerBlo.FindAll();

            if (owners != null)
            {
                return new WCFResponse<List<OwnerContract>>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = owners.Select(owner => new OwnerContract
                    {
                        Id = owner.Id,
                        Username = owner.Username,
                        Password = owner.Password,
                        Name = owner.Name,
                        LastName = owner.LastName,
                        BirthDate = owner.BirthDate,
                        DNI = owner.DNI,
                        EMail = owner.EMail,
                        Address = owner.Address,
                        PhoneNumber = owner.PhoneNumber,
                        ProfilePicture = owner.ProfilePicture,
                        DistrictId = owner.DistrictId
                    }).ToList()
                };
            }
            else
            {
                return new WCFResponse<List<OwnerContract>>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }

        public WCFResponse<OwnerContract> Login(OwnerContract owner)
        {
            Owner ownerEntity = new Owner { Username = owner.Username, Password = owner.Password };
            ownerEntity = ownerBlo.Login(ownerEntity);

            if (ownerEntity != null)
            {
                return new WCFResponse<OwnerContract>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = new OwnerContract
                    {
                        Id = ownerEntity.Id,
                        Username = ownerEntity.Username,
                        Password = ownerEntity.Password,
                        Name = ownerEntity.Name,
                        LastName = ownerEntity.LastName,
                        BirthDate = ownerEntity.BirthDate,
                        DNI = ownerEntity.DNI,
                        EMail = ownerEntity.EMail,
                        Address = ownerEntity.Address,
                        PhoneNumber = ownerEntity.PhoneNumber,
                        ProfilePicture = ownerEntity.ProfilePicture,
                        DistrictId = ownerEntity.DistrictId
                    }
                };
            }
            else
            {
                return new WCFResponse<OwnerContract>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = null
                };
            }
        }
    }
}





//string fullPath = "";

//if (!string.IsNullOrWhiteSpace(toInsert.ImageBase64) && !string.IsNullOrWhiteSpace(toInsert.ImageExtension))
//{
//    string filePath = $"{genId}_{toInsert.Name}_{toInsert.LastName}_{DateTime.Now.Ticks}.{toInsert.ImageExtension}";
//    fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{UPLOAD_FOLDER}\\{filePath}";
//    string serverPath = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{filePath}";
//    File.WriteAllBytes(fullPath, Convert.FromBase64String(toInsert.ImageBase64));
//    ownerEntity.ProfilePicture = serverPath;
//}

////AT THIS POINT INSERTION WAS SUCCESSFUL
//bool result = true;
//if (File.Exists(fullPath))
//{
//    result = ownerBlo.Update(ownerEntity);
//}

//if (!string.IsNullOrWhiteSpace(toUpdate.ImageBase64) && !string.IsNullOrWhiteSpace(toUpdate.ImageExtension))
//{
//    string fileName = $"{ownerEntity.Id}_{ownerEntity.Name}_{ownerEntity.LastName}_{DateTime.Now.Ticks}.{toUpdate.ImageExtension}";
//    fullPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{UPLOAD_FOLDER}\\{fileName}";
//    string serverPath = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{UPLOAD_FOLDER}/{fileName}";

//    File.WriteAllBytes(fullPath, Convert.FromBase64String(toUpdate.ImageBase64));

//    if (File.Exists(ownerEntity.ProfilePicture))
//        File.Delete(ownerEntity.ProfilePicture);
//    ownerEntity.ProfilePicture = serverPath;
//}