﻿using PawsBussinessLogic.DataTransferObject;
using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PawsWCF.Contract
{
    [DataContract]
    public class PetAdopterDtoContract
    {
        [DataMember(Order = 1)] public PetDto Pet { get; set; }
        [DataMember(Order = 2)] public Owner Adopter { get; set; }
        [DataMember(Order = 3)] public DateTime RequestDate { get; set; }
        [DataMember(Order = 4)] public DateTime ResponseDate { get; set; }
        [DataMember(Order = 5)] public bool State { get; set; }
    }
}