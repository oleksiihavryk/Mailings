namespace Mailings.Web.Domain.ServicesModels;
public sealed class Mail
{
    public Guid Id { get; set; } = Guid.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public IEnumerable<Attachment> Attachments { get; set; } =
        new List<Attachment>();

    //Equals override
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is Mail other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(Id, UserId, Content, Theme, Attachments);

    private bool Equals(Mail other)
        => Id.Equals(other.Id) && UserId == other.UserId &&
           Content == other.Content && Theme == other.Theme && 
           Attachments.Equals(other.Attachments);

}