namespace Mailings.Web.ViewModels;
/// <summary>
///     Mailing settings request
/// </summary>
public sealed class MailingRequestViewModel
{
    /// <summary>
    ///     Mailing identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    ///     Mailing name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///     Mailing mail type
    /// </summary>
    public string MailType { get; set; }
}