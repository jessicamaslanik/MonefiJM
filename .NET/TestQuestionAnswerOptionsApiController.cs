using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests.TestQuestionAnswerOptions;
using Sabio.Models.Requests.TestQuestions;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/testquestionansweroptions")]
    [ApiController]
    public class TestQuestionAnswerOptionsApiController : BaseApiController
    {
        private ITestQuestionAnswerOptionsService _service = null;
        private IAuthenticationService<int> _authService = null;

        public TestQuestionAnswerOptionsApiController(ITestQuestionAnswerOptionsService service
                                         , ILogger<PingApiController> logger
                                         , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }
        [HttpPost]
        public ActionResult<ItemResponse<int>> AddTestQuestionAnswerOption(TestQuestionAnswerOptionsAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                //int userId = _authService.GetCurrentUserId();
                IUserAuthData user = _authService.GetCurrentUser();
                model.CreatedBy = user.Id;
                int id = _service.AddTestQuestionAnswerOption(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                response.Item = id;

                result = Created201(response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> UpdateTestQuestionAnswerOption(TestQuestionAnswerOptionsUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                
                IUserAuthData user = _authService.GetCurrentUser();
                model.CreatedBy = user.Id;
                _service.UpdateTestQuestionAnswerOption(model);
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
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.DeleteTestQuestionAnswerOption(id);

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
