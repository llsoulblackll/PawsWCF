using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class AdoptionContract
    {
        [DataMember(Order = 1)]public int Id { get; set; }
        [DataMember(Order = 2)]public bool State { get; set; }
        [DataMember(Order = 3)]public string Description { get; set; }
        [DataMember(Order = 4)]public string Address { get; set; }
        [DataMember(Order = 5)]public string Age { get; set; }
        [DataMember(Order = 6)]public int TotalQuantity { get; set; }
        [DataMember(Order = 7)]public int AvailableQuantity { get; set; }
        [DataMember(Order = 8)]public DateTime PublishDate { get; set; }
        [DataMember(Order = 9)]public int DistrictId { get; set; }
        [DataMember(Order = 10)]public int OwnerId { get; set; }
        [DataMember(Order = 11)]public int SpecieId { get; set; }
        [DataMember(Order = 12)]public int RaceId { get; set; }
        [DataMember(Order = 13)]public int PetId { get; set; }
    }
}