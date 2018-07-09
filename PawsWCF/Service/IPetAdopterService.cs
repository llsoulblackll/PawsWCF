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
    public interface IPetAdopterService : IEntityService<PetAdopterContract>
    {
        [OperationContract(
            Name = "FindPetAdopter")]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Find/{petId}/{adopterId}")]
        WCFResponse<PetAdopterContract> Find(string petId, string adopterId);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllRequests/{ownerId}")]
        WCFResponse<List<PetAdopterContract>> FindAllRequests(string ownerId);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllAnswers/{adopterId}")]
        WCFResponse<List<PetAdopterContract>> FindAllAnswers(string adopterId);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllRequestsDto/{ownerId}")]
        WCFResponse<List<PetAdopterDtoContract>> FindAllRequestsDto(string ownerId);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllAnswersDto/{adopterId}")]
        WCFResponse<List<PetAdopterDtoContract>> FindAllAnswersDto(string adopterId);
    }
}
