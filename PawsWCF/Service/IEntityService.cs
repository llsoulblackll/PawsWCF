using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using PawsWCF.Contract;

namespace PawsWCF.Service
{
    [ServiceContract]
    public interface IEntityService<T>
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "New")]
        WCFResponse<object> New(T toInsert);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Update")]
        WCFResponse<object> Update(T toUpdate);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Delete/{id}")]
        WCFResponse<object> Delete(string id);

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Find/{id}")]
        WCFResponse<T> Find(string id);//SHOULD BE INT BUT WCF DOES NOT ALLOW URL PARAMS TO BE ANYTHING OTHER THAN STRING

        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FindAll")]
        WCFResponse<List<T>> FindAll();
    }
}