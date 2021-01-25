using Feedback.API.DTOs;
using Feedback.API.Model.Survey.SurveyEntry;
using General;
using System;
using System.Collections.Generic;

namespace Feedback.API.Model.Survey
{
    public class Survey : Entity<int>
    {
        public List<SurveySection> SurveySections { get; private set; }

        public Survey(List<SurveySection> surveySections)
        {
            SurveySections = surveySections;
        }

        public bool AnswerSurvey(List<AnsweredQuestionDTO> answeredQuestions)
        {
            List<SurveySection> SurveySectionBackup = new List<SurveySection>(SurveySections);
            foreach(AnsweredQuestionDTO answer in answeredQuestions)
            {
                foreach(SurveySection surveySection in SurveySections)
                {
                    if(answer.SurveySectionId == surveySection.Id)
                    {
                        if(!surveySection.RateQuestion(answer.QuestionId, answer.Answer))
                        {
                            SurveySections = SurveySectionBackup;
                            return false;
                        }

                        break;
                    }
                }
            }
            return true;
        }



    }
}