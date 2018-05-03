using System.Runtime.Serialization;
using PawsWCF.WCFConstant;

namespace PawsWCF.Contract
{
    [DataContract]
    public class WCFResponse<T>
    {
        [DataMember(Order = 1)]
        public WCFResponseCode ResponseCode { get; set; }
        [DataMember(Order = 2)]
        public string ResponseMessage { get; set; }
        [DataMember(Order = 3)]
        public T Response { get; set; }
    }
}