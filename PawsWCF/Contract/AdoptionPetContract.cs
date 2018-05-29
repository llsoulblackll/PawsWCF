using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class AdoptionPetContract
    {
        [DataMember(Order = 1)]public int AdoptionId { get; set; }
        [DataMember(Order = 2)] public int PetId { get; set; }
    }
}