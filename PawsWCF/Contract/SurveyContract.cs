using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class SurveyContract
    {
        [DataMember(Order = 1)]public int Id { get; set; }
        [DataMember(Order = 2)]public string AmountOfPeople { get; set; }
        [DataMember(Order = 3)]public string HomeDescription { get; set; }
        [DataMember(Order = 4)]public bool OtherPets { get; set; }
        [DataMember(Order = 5)]public string OtherPetsDescription { get; set; }
        [DataMember(Order = 6)]public string WorkType { get; set; }
        [DataMember(Order = 7)]public string Availability { get; set; }
        [DataMember(Order = 8)]public int OwnerId { get; set; }
    }
}