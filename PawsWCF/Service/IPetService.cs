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
    }
}
