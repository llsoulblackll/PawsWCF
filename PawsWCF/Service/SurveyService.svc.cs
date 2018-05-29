using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PawsBussinessLogic.BussinessLogicObject;
using PawsEntity;
using PawsWCF.Contract;
using PawsWCF.WCFConstant;

namespace PawsWCF.Service
{
    public class SurveyService : ISurveyService
    {
        private SurveyBlo surveyBlo = BloFactory.GetSurveyBlo();

        public WCFResponse<object> New(SurveyContract toInsert)
        {
            var survey = new Survey
            {
                AmountOfPeople = toInsert.AmountOfPeople,
                HomeDescription = toInsert.HomeDescription,
                OtherPets = toInsert.OtherPets,
                OtherPetsDescription = toInsert.OtherPetsDescription,
                WorkType = toInsert.WorkType,
                Availability = toInsert.Availability,
                OwnerId = toInsert.OwnerId
            };

            var result = surveyBlo.Insert(survey);

            if (result > 0)
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Success,
                    ResponseMessage = WCFResponseMessage.WCF_SUCCESS,
                    Response = result
                };
            }
            else
            {
                return new WCFResponse<object>
                {
                    ResponseCode = WCFResponseCode.Error,
                    ResponseMessage = WCFResponseMessage.WCF_ERROR,
                    Response = result
                };
            }
            
        }

        public WCFResponse<object> Update(SurveyContract toUpdate)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<object> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<SurveyContract> Find(string id)
        {
            throw new NotImplementedException();
        }

        public WCFResponse<List<SurveyContract>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
