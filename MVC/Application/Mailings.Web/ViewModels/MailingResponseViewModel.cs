namespace Mailings.Web.ViewModels;
/// <summary>
///     Mailing settings response
/// </summary>
public sealed class MailingResponseViewModel
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
    ///     Operation result
    /// </summary>
    public bool IsSuccess { get; set; }
}