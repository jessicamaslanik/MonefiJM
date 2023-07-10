using Sabio.Models.Requests.TestQuestionAnswerOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Interfaces
{
    public interface ITestQuestionAnswerOptionsService
    {
        public int AddTestQuestionAnswerOption(TestQuestionAnswerOptionsAddRequest model);
        public void UpdateTestQuestionAnswerOption(TestQuestionAnswerOptionsUpdateRequest model);
        public void DeleteTestQuestionAnswerOption(int id);
    }
}
