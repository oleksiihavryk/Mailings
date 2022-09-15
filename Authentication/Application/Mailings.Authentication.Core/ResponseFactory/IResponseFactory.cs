namespace Mailings.Authentication.Core.ResponseFactory;
/// <summary>
///     Interface of response factory.
/// </summary>
public interface IResponseFactory
{
    /// <summary>
    ///     Property which returned empty success response.
    /// </summary>
    public Response EmptySuccess { get; }
    /// <summary>
    ///     Property which returned empty status code 500 response.
    /// </summary>
    public Response EmptyInternalServerError { get; }

    /// <summary>
    ///     Method of creating a new API success response object
    /// </summary>
    /// <param name="successType">
    ///     Success response type
    /// </param>
    /// <param name="result">
    ///     Returned in response body object
    /// </param>
    /// <returns>
    ///     Response object
    /// </returns>
    public Response CreateSuccess(
        SuccessResponseType successType = SuccessResponseType.Ok,
        object? result = null);
    /// <summary>
    ///     Method for creating a new API failed response object
    /// </summary>
    /// <param name="failedType">
    ///     Failed response type
    /// </param>
    /// <param name="message">
    ///     Message of failed response type
    /// </param>
    /// <returns>
    ///     Response object
    /// </returns>
    public Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string? message = null);
    /// <summary>
    ///     Method for creating a new API failed response object
    /// </summary>
    /// <param name="failedType">
    ///     Failed response type
    /// </param>
    /// <param name="messages">
    ///     Messages of failed response type
    /// </param>
    /// <returns>
    ///     Response object
    /// </returns>
    public Response CreateFailedResponse(
        FailedResponseType failedType = FailedResponseType.BadRequest,
        string[]? messages = null);
}