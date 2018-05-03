using System;

namespace PawsEntity
{
    public class LostPet
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime LostDate { get; set; }
        public DateTime FoundDate { get; set; }
        public string Address { get; set; }
        public int DistrictId { get; set; }
        public int OwnerId { get; set; }
        public int FoundById { get; set; }
        public int PetId { get; set; }
    }
}
