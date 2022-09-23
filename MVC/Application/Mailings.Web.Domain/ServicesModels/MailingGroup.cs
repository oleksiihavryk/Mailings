namespace Mailings.Web.Domain.ServicesModels;
/// <summary>
///     Mailing group model
/// </summary>
public sealed class MailingGroup
{
    /// <summary>
    ///     Mailing group identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    /// <summary>
    ///     Mail identifier of mailing group
    /// </summary>
    public Guid MailId { get; set; } = Guid.Empty;
    /// <summary>
    ///     User identifier of mailing group
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    ///     Mailing group name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Mailing mail type 
    /// </summary>
    public string MailType { get; set; } = string.Empty;
    /// <summary>
    ///     Pseudo of sender
    /// </summary>
    public string SenderPseudo { get; set; } = string.Empty;
    /// <summary>
    ///     Email addresses which is receive your mail
    /// </summary>
    public IEnumerable<string> To { get; set; } = Array.Empty<string>();
}