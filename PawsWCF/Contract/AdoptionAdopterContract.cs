using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class AdoptionAdopterContract
    {
        [DataMember(Order = 1)]public int AdoptionId { get; set; }
        [DataMember(Order = 2)]public int AdopterId { get; set; }
        [DataMember(Order = 3)]public int AdoptedQuantity { get; set; }
        [DataMember(Order = 4)]public DateTime AdoptedDate { get; set; }
    }
}