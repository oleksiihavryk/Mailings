namespace Mailings.Web.Domain.ServicesModels;
/// <summary>
///     Mail model
/// </summary>
public sealed class Mail
{
    /// <summary>
    ///     Mail identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    /// <summary>
    ///     User id of this mail model
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    /// <summary>
    ///     Mail content
    /// </summary>
    public string Content { get; set; } = string.Empty;
    /// <summary>
    ///     Mail theme
    /// </summary>
    public string Theme { get; set; } = string.Empty;
    /// <summary>
    ///     Mail attachments
    /// </summary>
    public IEnumerable<Attachment> Attachments { get; set; } =
        new List<Attachment>();

    //Equals override
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is Mail other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(Id, UserId, Content, Theme, Attachments);

    /// <summary>
    ///     Equal operation between mails 
    /// </summary>
    /// <param name="other">
    ///     Other mail model
    /// </param>
    /// <returns>
    ///     Is equal current comparing objects
    /// </returns>
    private bool Equals(Mail other)
        => Id.Equals(other.Id) && UserId == other.UserId &&
           Content == other.Content && Theme == other.Theme && 
           Attachments.Equals(other.Attachments);
}