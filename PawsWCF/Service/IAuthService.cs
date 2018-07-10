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
    public interface IAuthService : IEntityService<AuthContract>
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Validate"
            )]
        WCFResponse<AuthContract> Validate(OwnerContract owner);

    }
}
