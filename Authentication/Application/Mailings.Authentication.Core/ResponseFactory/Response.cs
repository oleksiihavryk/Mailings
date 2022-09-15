using Microsoft.AspNetCore.Http;

namespace Mailings.Authentication.Core.ResponseFactory;

/// <summary>
///     DTO of API response
/// </summary>
public sealed class Response
{
    /// <summary>
    ///     Is success result of API response
    /// </summary>
    public bool IsSuccess { get; set; } = false;
    /// <summary>
    ///     Status code of API response
    /// </summary>
    public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
    /// <summary>
    ///     Body object of API response
    /// </summary>
    public object? Result { get; set; } = null;
    /// <summary>
    ///     Multiplicity of messages from API response
    /// </summary>
    public IEnumerable<string> Messages { get; set; } = Enumerable.Empty<string>();
}