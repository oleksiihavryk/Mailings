using Mailings.AuthenticationService.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Mailings.AuthenticationService.Core.ResponseFactory;
/// <summary>
///     Implementation of response factory
/// </summary>
public class ResponseFactory : IResponseFactory
{
    /// <summary>
    ///     Implementation of empty success api result
    /// </summary>
    public virtual Response EmptySuccess => new Response()
    {
        Result = null,
        IsSuccess = true,
        Messages = Array.Empty<string>(),
        StatusCode = StatusCodes.Status204NoContent
    };
    /// <summary>
    ///     Implementation of empty internal server error (Status code: 500) api result
    /// </summary>
    public virtual Response EmptyInternalServerError => new Response()
    {
        Result = null,
        IsSuccess = false,
        Messages = Array.Empty<string>(),
        StatusCode = StatusCodes.Status500InternalServerError
    };

    /// <summary>
    ///     Creating a new success result with defined success type and body object result
    /// </summary>
    /// <param name="successType">
    ///     Success type of api result,
    ///     an example: created (status code: 201), empty content (status code: 204))
    /// </param>
    /// <param name="result">
    ///     Body object of api result
    /// </param>
    /// <returns>
    ///     Response object (api result model)
    /// </returns>
    /// <exception cref="UnknownResponseTypeException">
    ///     Inner exception which means Success response type is unhandled inside the system.
    /// </exception>
    public virtual Response CreateSuccess(
        SuccessResponseType successType = SuccessResponseType.Ok,
        object? result = null)
        => new Response()
        {
            Result = result,
            IsSuccess = true,
            Messages = Array.Empty<string>(),
            StatusCode = successType switch
            {
                SuccessResponseType.Ok => StatusCodes.Status200OK,
                SuccessResponseType.MissingResult => StatusCodes.Status204NoContent,
                SuccessResponseType.Changed => StatusCodes.Status205ResetContent,
                SuccessResponseType.Created => StatusCodes.Status201Created,
                _ => throw new UnknownResponseTypeException()
            }
        };
    /// <summary>
    ///     Creating a new failed api result with choosen failed type and some included message
    /// </summary>
    /// <param name="failedType">
    ///     Type of api response,
    ///     an example: bad request(status code: 400), not found (status code: 404)
    /// </param>
    /// <param name="message">
    ///     Message which send with api result
    /// </param>
    /// <returns>
    ///     Response object
    /// </returns>
    /// <exception cref="UnknownResponseTypeException">
    ///     Inner exception which means Success response type is unhandled inside the system.
    /// </exception>
    public virtual Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string? message = null)
        => new Response()
        {
            Result = null,
            Messages = new [] { message ?? string.Empty },
            IsSuccess = false,
            StatusCode = failedType switch
            {
                FailedResponseType.BadRequest => StatusCodes.Status400BadRequest,
                FailedResponseType.NotFound => StatusCodes.Status404NotFound,
                _ => throw new UnknownResponseTypeException()
            }
        };
    /// <summary>
    ///     Creating a new failed api result with choosen failed type and some included message
    /// </summary>
    /// <param name="failedType">
    ///     Type of api response,
    ///     an example: bad request(status code: 400), not found (status code: 404)
    /// </param>
    /// <param name="messages">
    ///     Messages which send with api result
    /// </param>
    /// <returns>
    ///     Response object
    /// </returns>
    /// <exception cref="UnknownResponseTypeException">
    ///     Inner exception which means Success response type is unhandled inside the system.
    /// </exception>
    public virtual Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string[]? messages = null)
        => new Response()
        {
            Result = null,
            Messages = messages ?? Array.Empty<string>(),
            IsSuccess = false,
            StatusCode = failedType switch
            {
                FailedResponseType.BadRequest => StatusCodes.Status400BadRequest,
                FailedResponseType.NotFound => StatusCodes.Status404NotFound,
                _ => throw new UnknownResponseTypeException()
            }
        };
}