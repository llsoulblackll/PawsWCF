using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsWCF.Contract;

namespace PawsWCF.Service
{
    [ServiceContract]
    public interface IFoundPetService : IEntityService<FoundPetContract>
    {
    }
}
