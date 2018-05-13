using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class FoundPetContract
    {
        [DataMember(Order = 1)]public int Id { get; set; }
        [DataMember(Order = 2)]public bool State { get; set; }
        [DataMember(Order = 3)]public string Description { get; set; }
        [DataMember(Order = 4)]public double Longitude { get; set; }
        [DataMember(Order = 5)]public double Latitude { get; set; }
        [DataMember(Order = 6)]public DateTime FoundDate { get; set; }
        [DataMember(Order = 7)]public DateTime DeliveredDate { get; set; }
        [DataMember(Order = 8)]public string Address { get; set; }
        [DataMember(Order = 9)]public int DistrictId { get; set; }
        [DataMember(Order = 10)]public int RaceId { get; set; }
        [DataMember(Order = 11)]public int FoundById { get; set; }
        [DataMember(Order = 12)]public int DeliveredToId { get; set; }
    }
}