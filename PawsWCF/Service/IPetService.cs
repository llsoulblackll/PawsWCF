using PawsWCF.Contract;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PawsWCF.Service
{
    [ServiceContract]
    public interface IPetService : IEntityService<PetContract>
    {
        [OperationContract(
            Name = "FindAllPets")]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAll/{ownerId}")]
        WCFResponse<List<PetContract>> FindAll(string ownerId);

        [OperationContract(
            Name = "FindAllPetsDto")]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllDto")]
        WCFResponse<List<PetDtoContract>> FindAllDto();

        [OperationContract(
            Name = "FindAllPetsDtoByOwnerId")]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAllDto/{ownerId}/{ownPets}")]
        WCFResponse<List<PetDtoContract>> FindAllDto(string ownerId, string ownPets);
    }
}
