using Mailings.Resources.API.Dto;
using Mailings.Resources.Data.Exceptions;
using Mailings.Resources.Data.Repositories;
using Mailings.Resources.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mailings.Resources.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/mails/html")]
    public sealed class HtmlMailsController : ControllerBase
    {
        private readonly IHtmlMailsRepository _htmlMailsRepository;
        private readonly IResponseFactory _responseFactory;

        public HtmlMailsController(
            IHtmlMailsRepository htmlMailsRepository, 
            IResponseFactory responseFactory)
        {
            _htmlMailsRepository = htmlMailsRepository;
            _responseFactory = responseFactory;
        }

        [HttpGet]
        public IActionResult GetAllHtmlMails()
        {
            var mails = _htmlMailsRepository.GetAll();
            var result = _responseFactory.CreateSuccess(mails);

            return Ok(result);
        }
        [HttpGet("/user-id/{userId}")]
        public IActionResult GetAllHtmlMailsByUserId([FromRoute]string? userId)
        {
            ResponseDto? result = null;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var mails = _htmlMailsRepository.GetAll();
                mails = mails.Where(x => x.UserId == userId);

                if (mails.Any())
                    result = _responseFactory.CreateSuccess(mails);
                else result = _responseFactory.CreateFailedResponse(
                        statusCode: StatusCodes.Status204NoContent,
                        message: "User with current is is not have any mails in system");
            }
            else
            {
                result = _responseFactory.CreateFailedResponse(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: "User id field in route is cannot be empty");
            }

            return Ok(result);
        }
        [HttpGet("/id/{id}")]
        public async Task<IActionResult> GetHtmlMailById([FromRoute]string id)
        {
            ResponseDto? result = null;

            if (Guid.TryParse(id, out var guid))
            {
                try
                {
                    var htmlMail = await _htmlMailsRepository.GetByKeyAsync(key: guid);
                    result = _responseFactory.CreateSuccess(htmlMail);
                }
                catch (ObjectNotFoundInDatabaseException)
                {
                    result = _responseFactory.CreateFailedResponse(
                        statusCode: StatusCodes.Status404NotFound,
                        message: "Mail with current id is not found in system");
                }
            }
            else
            {
                result = _responseFactory.CreateFailedResponse(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: "Id field in route is cannot be empty");
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> SaveHtmlMail(
            [FromBody][FromForm] HtmlMailDto mail)
        {
            var updatedEntity = await _htmlMailsRepository
                .SaveIntoDbAsync(entity: mail);
            var result = _responseFactory.CreateSuccess(updatedEntity);

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInDatabaseHtmlMail(
            [FromBody][FromForm] HtmlMailDto mail)
        {
            var updatedEntity = await _htmlMailsRepository
                .SaveIntoDbAsync(entity: mail);
            var result = _responseFactory.CreateSuccess(updatedEntity);

            return Ok(result);
        }
        [HttpDelete("/id/{id}")]
        public async Task<IActionResult> DeleteMail(
            [FromRoute] string id)
        {
            ResponseDto? result = null;

            if (Guid.TryParse(id, out var guid))
            {
                try
                {
                    await _htmlMailsRepository.DeleteFromDbByKey(key: guid);
                    result = _responseFactory.CreateSuccess();
                }
                catch (ObjectNotFoundInDatabaseException)
                {
                    result = _responseFactory.CreateFailedResponse(
                        statusCode: StatusCodes.Status404NotFound,
                        message: "Mail with current id is not found in system");
                }
            }
            else
            {
                result = _responseFactory.CreateFailedResponse(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: "Id field in route is cannot be empty");
            }

            return Ok(result);
        }
    }
}
