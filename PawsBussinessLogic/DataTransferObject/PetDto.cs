using PawsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsBussinessLogic.DataTransferObject
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime PublishDate { get; set; }
        public bool State { get; set; }
        public string OtherRace { get; set; }
        public Specie Specie { get; set; }
        public Race Race { get; set; }
        public Owner Owner { get; set; }
    }
}
