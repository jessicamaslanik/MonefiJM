using Sabio.Data.Providers;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests.TestQuestionAnswerOptions;
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
    public class TestQuestionAnswerOptionsService:ITestQuestionAnswerOptionsService
    {
        IDataProvider _data = null;
        public TestQuestionAnswerOptionsService(IDataProvider data)
        {
            _data = data;
        }

        public int AddTestQuestionAnswerOption(TestQuestionAnswerOptionsAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[TestQuestionAnswerOptions_Insert]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                

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
        public void UpdateTestQuestionAnswerOption(TestQuestionAnswerOptionsUpdateRequest model)
        {
            string procName = "[dbo].[TestQuestionAnswerOptions_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                    
                },
                returnParameters: null
                );
        }

        public void DeleteTestQuestionAnswerOption(int id)
        {
            string procName = "[dbo].[TestQuestionAnswerOptions_DeleteById]";
            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);
            });
        }

        private static void AddCommonParams(TestQuestionAnswerOptionsAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@QuestionId", model.QuestionId);
            col.AddWithValue("@Text", model.Text);
            col.AddWithValue("@Value", model.Value);
            col.AddWithValue("@AdditionalInfo", model.AdditionalInfo);
            col.AddWithValue("@IsCorrect", model.IsCorrect);
            col.AddWithValue("@CreatedBy", model.CreatedBy);

        }

    }
}
