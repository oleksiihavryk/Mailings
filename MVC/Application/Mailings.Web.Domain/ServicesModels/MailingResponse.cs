namespace Mailings.Web.Domain.ServicesModels;
/// <summary>
///     Mailing setup response 
/// </summary>
public sealed class MailingResponse
{
    /// <summary>
    ///     Configured is successfully
    /// </summary>
    public bool IsSuccess { get; set; }
    /// <summary>
    ///     Mailing identifier
    /// </summary>
    public Guid MailingId { get; set; }
}