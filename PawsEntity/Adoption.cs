using System;

namespace PawsEntity
{
    public class Adoption
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DistrictId { get; set; }
        public int OwnerId { get; set; }
        public int RaceId { get; set; }
        public int PetId { get; set; }
    }
}
