using PawsWCF.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PawsWCF.Service
{
    [ServiceContract]
    public interface IAdoptionService : IEntityService<AdoptionContract>
    {
    }
}
