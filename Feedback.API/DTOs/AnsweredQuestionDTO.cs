using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.DTOs
{
    public class AnsweredQuestionDTO
    {
        public int SurveySectionId { get; set; }
        public int QuestionId { get; set; }
        public int Answer { get; set; }
    }
}
