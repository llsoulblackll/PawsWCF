
namespace PawsEntity
{
    public class Survey
    {
        public int Id { get; set; }
        public string AmountOfPeople { get; set; }
        public string HomeDescription { get; set; }
        public bool OtherPets { get; set; }
        public string OtherPetsDescription { get; set; }
        public string WorkType { get; set; }
        public string Availability { get; set; }
        public int OwnerId { get; set; }
    }
}
