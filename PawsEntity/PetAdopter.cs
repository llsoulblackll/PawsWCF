using System;

namespace PawsEntity
{
    public class PetAdopter
    {
        public int PetId { get; set; }
        public int AdopterId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
        public bool State { get; set; }
    }
}
