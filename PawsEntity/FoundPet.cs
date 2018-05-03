using System;

namespace PawsEntity
{
    public class FoundPet
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public string Description { get; set; }
        public string Age { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime FoundDate { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string Address { get; set; }
        public int DistrictId { get; set; }
        public int RaceId { get; set; }
        public int FoundById { get; set; }
        public int DeliveredToId { get; set; }
    }
}
