using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class DistrictContract
    {
        [DataMember(Order = 1)]public int Id { get; set; }
        [DataMember(Order = 2)]public string Name { get; set; }
        //[DataMember(Order = 3)]public double Longitude { get; set; }
        //[DataMember(Order = 4)]public double Latitude { get; set; }
    }
}