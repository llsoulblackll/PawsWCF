using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    /// <summary>
    /// This class should be deprecated in the future and replaced by AdoptionAdopterContract
    /// </summary>
    [DataContract]
    public class PetAdopterContract
    {
        [DataMember(Order = 1)]public int PetId { get; set; }
	    [DataMember(Order = 2)]public int AdopterId { get; set; }
	    [DataMember(Order = 3)]public DateTime RequestDate { get; set; }
        [DataMember(Order = 4)]public DateTime ResponseDate { get; set; }
        [DataMember(Order = 5)]public bool State { get; set; }
    }
}