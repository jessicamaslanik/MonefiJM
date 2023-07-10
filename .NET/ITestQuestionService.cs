using Sabio.Models.Requests.TestQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Interfaces
{
    public interface ITestQuestionService
    {
        int AddTestQuestion(TestQuestionAddRequest model);
        void UpdateTestQuestion(TestQuestionUpdateRequest model);
        void DeleteTestQuestion(int id);

    }
}
