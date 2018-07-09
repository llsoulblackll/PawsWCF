using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsBussinessLogic.DataTransferObject
{
    public class PetAdopterDto
    {
        public PetDto Pet { get; set; }
        public Owner Adopter { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
        public bool State { get; set; }
    }
}
