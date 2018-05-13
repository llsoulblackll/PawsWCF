﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class RaceContract
    {
        [DataMember(Order = 1)]public int Id { get; set; }
        [DataMember(Order = 2)]public string Name { get; set; }
        [DataMember(Order = 3)]public int SpecieId { get; set; }
    }
}