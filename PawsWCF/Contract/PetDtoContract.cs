using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class PetDtoContract
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public string Age { get; set; }
        [DataMember(Order = 4)] public string Description { get; set; }
        [DataMember(Order = 5)] public string Picture { get; set; }
        [DataMember(Order = 6)] public DateTime PublishDate { get; set; }
        [DataMember(Order = 7)] public bool State { get; set; }
        [DataMember(Order = 8)] public string OtherRace { get; set; }
        [DataMember(Order = 9)] public Specie Specie { get; set; }
        [DataMember(Order = 10)] public Race Race { get; set; }
        [DataMember(Order = 11)] public Owner Owner { get; set; }
    }
}