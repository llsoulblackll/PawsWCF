using System;

namespace PawsEntity
{
    public class AdoptionAdopter
    {
        public int AdoptionId { get; set; }
        public int AdopterId { get; set; }
        public int AdoptedQuantity { get; set; }
        public DateTime AdoptedDate { get; set; }
    }
}
