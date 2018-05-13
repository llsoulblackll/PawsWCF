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
    public interface IAdoptionAdopterService : IEntityService<AdoptionAdopterContract>
    {
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAll/{adoptionId}")]
        WCFResponse<List<AdoptionAdopterContract>> FindAll(string adoptionId);
    }
}
