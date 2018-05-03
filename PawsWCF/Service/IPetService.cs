using PawsWCF.Contract;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PawsWCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPetService" in both code and config file together.
    [ServiceContract]
    public interface IPetService
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "NewPet")]
        WCFResponse<object> NewPet(PetContract pet);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdatePet")]
        WCFResponse<object> UpdatePet(PetContract pet);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DeletePet/{id}")]
        WCFResponse<object> DeletePet(string id);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindPet/{id}")]
        WCFResponse<PetContract> FindPet(string id);//SHOULD BE INT BUT WCF DOES NOT ALLOW URL PARAMS TO BE ANYTHING OTHER THAN STRING

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllPets")]
        WCFResponse<List<PetContract>> FindAllPets();
    }
}
