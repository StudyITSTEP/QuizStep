using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.DTOs.Quiz
{
    public class QuizParticipantProgressDto
    {
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public int CurrentQuestion { get; set; }
        public int TotalQuestions { get; set; }
    }
}
