
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public LookUp Type { get; set; }
        public string Question { get; set; }
        public string HelpText { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMultipleAllowed { get; set; }
        public LookUp QuestionType { get; set; }
        public int TestId { get; set; }
        public LookUp Status { get; set; }
        public int SortOrder { get; set; }
        public List<TestQuestionAnswerOptions> AnswerOptions { get; set; }
        public int StatusId { get; set; }

        
   }
}
