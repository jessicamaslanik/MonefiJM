using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.TestQuestions
{
    public class TestQuestionAddRequest
    {

        [Required]
        public int UserId { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string HelpText { get; set; }
        [Required]
        public bool IsRequired { get; set; }
        [Required]
        public bool IsMultipleAllowed { get; set; }
        [Required]
        public int QuestionTypeId { get; set; }
        [Required]
        public int TestId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int SortOrder { get; set; }
       
    }
}
