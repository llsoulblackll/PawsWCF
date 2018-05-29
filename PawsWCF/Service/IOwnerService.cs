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
    public interface IOwnerService : IEntityService<OwnerContract>
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Login")]
        WCFResponse<OwnerContract> Login(OwnerContract owner);
    }
}
