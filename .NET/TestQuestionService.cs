using Sabio.Data.Providers;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests.TestQuestions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.TestQuestions
{
    public class TestQuestionService:ITestQuestionService
    {
        IDataProvider _data = null;

        public TestQuestionService(IDataProvider data)
        {
            _data = data;
        }

        public int AddTestQuestion(TestQuestionAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[TestQuestions_Insert]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@TestId", model.TestId);
                    
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);

                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;

                    int.TryParse(oId.ToString(), out id);
                });
            return id;
        }

        public void UpdateTestQuestion(TestQuestionUpdateRequest model)
        {
            string procName = "[dbo].[TestQuestions_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                    
                },
                returnParameters: null
                ); 
        }
        public void DeleteTestQuestion(int id)
        {
            string procName = "[dbo].[TestQuestions_DeleteById]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    
                    col.AddWithValue("@Id", id);
                },
                returnParameters: null
              );
        }

        private static void AddCommonParams(TestQuestionAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Question", model.Question);
            col.AddWithValue("@HelpText", model.HelpText);
            col.AddWithValue("@IsRequired", model.IsRequired);
            col.AddWithValue("@IsMultipleAllowed", model.IsMultipleAllowed);
            col.AddWithValue("@QuestionTypeId", model.QuestionTypeId);
            col.AddWithValue("@StatusId", model.StatusId);
            col.AddWithValue("@SortOrder", model.SortOrder);
        }

    }
}
