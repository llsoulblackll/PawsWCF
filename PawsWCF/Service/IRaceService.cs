using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PawsWCF.Contract;

namespace PawsWCF.Service
{
    [ServiceContract]
    public interface IRaceService : IEntityService<RaceContract>
    {
        [OperationContract(
            Name = "FindAllById")]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAll/{specieId}")]
        WCFResponse<List<RaceContract>> FindAll(string specieId);
    }
}
