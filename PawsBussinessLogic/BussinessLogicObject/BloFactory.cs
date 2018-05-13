
namespace PawsBussinessLogic.BussinessLogicObject
{
    public static class BloFactory
    {
        public static DistrictBlo GetDistrictBlo()
        {
            return new DistrictBlo();
        }

        public static SpecieBlo GetSpecieBlo()
        {
            return new SpecieBlo();
        }

        public static RaceBlo GetRaceBlo()
        {
            return new RaceBlo();
        }

        public static AdoptionBlo GetAdoptionBlo()
        {
            return new AdoptionBlo();
        }

        public static PetBlo GetPetBlo()
        {
            return new PetBlo();
        }

        public static OwnerBlo GetOwnerBlo()
        {
            return new OwnerBlo();
        }

        public static AdoptionAdopterBlo GetAdoptionAdopterBlo()
        {
            return new AdoptionAdopterBlo();
        }

        public static LostPetBlo GetLostPetBlo()
        {
            return new LostPetBlo();
        }

        public static FoundPetBlo GetFoundPetBlo()
        {
            return new FoundPetBlo();
        }
    }
}
