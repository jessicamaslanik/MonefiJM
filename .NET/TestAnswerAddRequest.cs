using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.TestAnswers
{
    public class TestAnswerAddRequest
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int InstanceId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int QuestionId { get; set; }
        [AllowNull]
        [Range(1, Int32.MaxValue)]
        public int? AnswerOptionId { get; set; }
        [AllowNull]
        [StringLength(500, MinimumLength = 0)]
        public string Answer { get; set; }
    }
}
