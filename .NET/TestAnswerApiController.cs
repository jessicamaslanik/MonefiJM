using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Requests.TestInstances;
using Sabio.Models;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using Sabio.Models.Requests.TestAnswers;
using Sabio.Services.Interfaces;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/testanswers")]
    [ApiController]
    public class TestAnswerApiController : BaseApiController
    {
        private ITestAnswerService _answerService = null;
        private IAuthenticationService<int> _authService = null;
        public TestAnswerApiController(ITestAnswerService service, ILogger<TestAnswerApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _answerService = service;
            _authService = authService;
        }
        [HttpPost]
        public ActionResult<ItemResponse<int>> AddTestAnswer(TestAnswerAddRequest model)
        {
            ObjectResult result = null;
            try
            {
                IUserAuthData user = _authService.GetCurrentUser();
                int id = _answerService.Create(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }
        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<int>> UpdateTestAnswer(TestAnswerUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                _answerService.Update(model);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
        [HttpDelete("{id:int}")]
        public ActionResult<ItemResponse<int>> DeleteById(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                _answerService.Delete(id);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
    }
}
