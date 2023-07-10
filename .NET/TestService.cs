using Newtonsoft.Json;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.TestQuestions;
using Sabio.Models.Requests;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sabio.Services {
    public class TestService : ITestService{
        IDataProvider _data = null;
        IBaseUserMapper _userMapper = null;
        ILookUpService _lookUp = null;

        public TestService(IDataProvider data, IBaseUserMapper userMapper, ILookUpService lookUp) {
            _data = data;
            _userMapper = userMapper;
            _lookUp = lookUp;
        }

        #region CREATE Methods
        public int Add(TestAddRequest model, int userId) {
            int newRecordId = 0;

            string procName = "[dbo].[Tests_Insert]";

            _data.ExecuteNonQuery(
                storedProc: procName,
                inputParamMapper: delegate (SqlParameterCollection inColl) {
                    SqlParameter recordIdInSql = new SqlParameter("@Id", SqlDbType.Int);
                    recordIdInSql.Direction = ParameterDirection.Output;

                    AddCommonParams(model, inColl);
                    inColl.AddWithValue("@CreatedBy", userId);
                    inColl.Add(recordIdInSql);
                }, returnParameters: delegate (SqlParameterCollection outColl) {
                    object recordIdSqlToApi = outColl["@Id"].Value;
                    int.TryParse(recordIdSqlToApi.ToString(), out newRecordId);
                });
            return newRecordId;
        }
        #endregion

        #region READ Methods
        public Paged<Test> GetAll(int pageIndex, int pageSize) {
            Paged<Test> pagedTestList = null;
            List<Test> testList = null;
            int totalCount = 0;

            string procName = "[dbo].[Tests_SelectAll]";
            _data.ExecuteCmd(
                storedProc: procName,
                inputParamMapper: delegate(SqlParameterCollection inColl) {
                    inColl.AddWithValue("@PageIndex", pageIndex);
                    inColl.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set) {
                    int startingIndex = 0;
                    Test singleTest = MapSingleTest(reader, ref startingIndex, _userMapper, _lookUp);
                    if(testList == null) {
                        testList = new List<Test>();
                    }
                    if(totalCount == 0) {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    testList.Add(singleTest);
                });

            if(testList != null) {
                pagedTestList = new Paged<Test>(testList, pageIndex, pageSize, totalCount);
            }

            return pagedTestList;
        }
        public Test GetByTestId(int testId) {
            Test test = null;
            string procName = "[dbo].[Tests_SelectById]";

            _data.ExecuteCmd(
                storedProc: procName,
                inputParamMapper: delegate (SqlParameterCollection inColl) {
                    inColl.AddWithValue("@Id", testId);
                }, singleRecordMapper: delegate (IDataReader reader, short set) {
                    int startingIndex = 0;
                    test = MapSingleTest(reader, ref startingIndex, _userMapper, _lookUp);
                });
            return test;
        }
        public Paged<Test> GetByCreatedId(int creatorId, int pageIndex, int pageSize) {
            Paged<Test> pagedTestList = null;
            List<Test> testList = null;
            int totalCount = 0;

            string procName = "[dbo].[Tests_Select_ByCreatedBy]";
            _data.ExecuteCmd(
                storedProc: procName,
                inputParamMapper: delegate (SqlParameterCollection inColl) {
                    inColl.AddWithValue("@CreatedBy", creatorId);
                    inColl.AddWithValue("@PageIndex", pageIndex);
                    inColl.AddWithValue("@PageSize", pageSize);
                }, singleRecordMapper: delegate(IDataReader reader, short set) {
                    int startingIndex = 0;
                    Test singleTest = MapSingleTest(reader, ref startingIndex, _userMapper, _lookUp);
                    if (testList == null) {
                        testList = new List<Test>();
                    }
                    if(totalCount == 0) {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    testList.Add(singleTest);
                });

            if(testList != null) {
                pagedTestList = new Paged<Test>(testList, pageIndex, pageSize, totalCount);
            }

            return pagedTestList;
        }

        public Paged<Test> Search(string query, int pageIndex, int pageSize) {
            Paged<Test> pagedTestList = null;
            List<Test> testList = null;
            int totalCount = 0;

            string procName = "[dbo].[Tests_Search]";
            _data.ExecuteCmd(
                storedProc: procName,
                inputParamMapper: delegate (SqlParameterCollection inColl) {
                    inColl.AddWithValue("@Query", query);
                    inColl.AddWithValue("@PageIndex", pageIndex);
                    inColl.AddWithValue("@PageSize", pageSize);
                }, singleRecordMapper: delegate (IDataReader reader, short set) {
                    int startingIndex = 0;
                    Test singleTest = MapSingleTest(reader, ref startingIndex, _userMapper, _lookUp);
                    if(testList == null) {
                        testList = new List<Test>();
                    }
                    if(totalCount == 0) {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    testList.Add(singleTest);
                });

            if(testList != null) {
                pagedTestList = new Paged<Test>(testList, pageIndex, pageSize, totalCount);
            }

            return pagedTestList;
        }
        #endregion

        #region UPDATE Methods
        public void Update(TestUpdateRequest model) {
            string procName = "[dbo].[Tests_Update]";

            _data.ExecuteNonQuery(
                storedProc: procName,
                inputParamMapper: delegate (SqlParameterCollection inColl) {
                    AddCommonParams(model, inColl);
                    inColl.AddWithValue("@Id", model.Id);
                }, returnParameters: null);
        }
        #endregion

        #region DELETE Methods
        public void DeleteById(int id) {
            string procName = "[dbo].[Tests_Delete_ById]";

            _data.ExecuteNonQuery(
                storedProc: procName,
                inputParamMapper: delegate (SqlParameterCollection inColl) {
                    inColl.AddWithValue("@Id", id);
                }, returnParameters: null);
        }
        #endregion

        #region private Methods
        private static void AddCommonParams(TestAddRequest model, SqlParameterCollection inColl) {
            inColl.AddWithValue("@Name", model.Name);
            inColl.AddWithValue("@Description", model.Description);
            inColl.AddWithValue("@StatusId", model.StatusId);
            inColl.AddWithValue("@TestTypeId", model.TestTypeId);
        }

        private static Test MapSingleTest(IDataReader reader, ref int startingIndex, IBaseUserMapper userMapper, ILookUpService lookUp) {
            Test test = new Test();

            test.Id = reader.GetSafeInt32(startingIndex++);
            test.Name = reader.GetSafeString(startingIndex++);
            test.Description = reader.GetSafeString(startingIndex++);
            test.Status = lookUp.MapSingleLookUp(reader, ref startingIndex);
            test.TestType = lookUp.MapSingleLookUp(reader, ref startingIndex);

            string questionsJson = reader.GetSafeString(startingIndex++);
            test.Questions = JsonConvert.DeserializeObject<List<TestQuestion>>(questionsJson);
            
            test.DateCreated = reader.GetSafeDateTime(startingIndex++);
            test.DateModified = reader.GetSafeDateTime(startingIndex++);
            test.CreatedBy = userMapper.MapBaseUser(reader, ref startingIndex);

            return test;
        }

        #endregion
    }
}
