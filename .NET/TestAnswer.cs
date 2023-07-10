using Sabio.Models.Domain.TestQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class TestAnswer
    {
        public int Id { get; set; }
        public int InstanceId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerOptionId { get; set; }
        public string Answer { get; set; }
    }
}
