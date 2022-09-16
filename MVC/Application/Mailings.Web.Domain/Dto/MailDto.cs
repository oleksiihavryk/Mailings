namespace Mailings.Web.Domain.Dto;
public sealed class MailDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public IEnumerable<AttachmentDto> Attachments { get; set; } =
        new List<AttachmentDto>();

    //Equals override
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is MailDto other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(Id, UserId, Content, Theme, Attachments);

    private bool Equals(MailDto other)
        => Id.Equals(other.Id) && UserId == other.UserId &&
           Content == other.Content && Theme == other.Theme && 
           Attachments.Equals(other.Attachments);

}