using PawsWCF.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PawsWCF.Service
{
    [ServiceContract]
    public interface IAdoptionPetService : IEntityService<AdoptionPetContract>
    {
        [OperationContract
            (Name = "NewAdoptionPet")]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "New"
            )]
        new WCFResponse<bool> New(AdoptionPetContract adoptionPet);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Delete/{adoptionId}/{petId}")]
        WCFResponse<bool> Delete(string adoptionId, string petId);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAll/{adoptionId}")]
        WCFResponse<List<AdoptionPetContract>> FindAll(object adoptionId);
    }
}
