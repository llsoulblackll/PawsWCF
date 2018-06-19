using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    public class OwnerDtoContract
    {
        [DataMember(Order = 1)]public int Id { get; set; }
        [DataMember(Order = 2)]public string Username { get; set; }
        [DataMember(Order = 3)]public string Password { get; set; }
        [DataMember(Order = 4)]public string Name { get; set; }
        [DataMember(Order = 5)]public string LastName { get; set; }
        [DataMember(Order = 6)]public DateTime BirthDate { get; set; }
        [DataMember(Order = 7)]public string DNI { get; set; }
        [DataMember(Order = 8)]public string EMail { get; set; }
        [DataMember(Order = 9)]public string Address { get; set; }
        [DataMember(Order = 10)]public string PhoneNumber { get; set; }
        [DataMember(Order = 11)]public string ProfilePicture { get; set; }
        [DataMember(Order = 12)]public District District { get; set; }
        [DataMember(Order = 13)]public List<Pet> Pets { get; set; }
    }
}