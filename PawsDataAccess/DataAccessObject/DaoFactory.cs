﻿using PawsDataAccess.DataAccessObject;
using PawsDataAccess.DataAccessObject.DataAccessObjectImpl;

namespace PawsDataAccess.DataAccessObject
{
    public static class DaoFactory
    {
        public static IDistrictDao GetDistrictDao()
        {
            return new DistrictDaoImpl();
        }

        public static ISpecieDao GetSpecieDao()
        {
            return new SpecieDaoImpl();
        }

        public static IRaceDao GetRaceDao()
        {
            return new RaceDaoImpl();
        }

        public static IAdoptionDao GetAdoptionDao()
        {
            return new AdoptionDaoImpl();
        }

        public static IPetDao GetPetDao()
        {
            return new PetDaoImpl();
        }

        public static IOwnerDao GetOwnerDao()
        {
            return new OwnerDaoImpl();
        }

        public static ISurveyDao GetSurveyDao()
        {
            return new SurveyDaoImpl();
        }

        public static IAdoptionPetDao GetAdoptionPetDao()
        {
            return new AdoptionPetDaoImpl();
        }

        public static IAdoptionAdopterDao GetAdoptionAdopterDao()
        {
            return new AdoptionAdopterDaoImpl();
        }

        public static ILostPetDao GetLostPetDao()
        {
            return new LostPetDaoImpl();
        }

        public static IFoundPetDao GetFoundPetDao()
        {
            return new FoundPetDaoImpl();
        }

        public static IPetAdopterDao GetPetAdopterDao()
        {
            return new PetAdopterDaoImpl();
        }

        public static IAuthDao GetAuthDao()
        {
            return new AuthDaoImpl();
        }
    }
}
