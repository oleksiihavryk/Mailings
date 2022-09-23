namespace Mailings.Web.Domain.ServicesModels;
/// <summary>
///     Mailing setup request
/// </summary>
public sealed class MailingRequest
{
    /// <summary>
    ///     Mailing identifier
    /// </summary>
    public Guid MailingId { get; set; }
    /// <summary>
    ///     Mailing type
    /// </summary>
    public string SendType { get; set; } = string.Empty;
}