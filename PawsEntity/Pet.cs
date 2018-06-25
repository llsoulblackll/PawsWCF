using System;

namespace PawsEntity
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime PublishDate { get; set; }
        public bool State { get; set; }
        public string OtherRace { get; set; }
        public int SpecieId { get; set; }
        public int RaceId { get; set; }
        public int OwnerId { get; set; }
    }
}
