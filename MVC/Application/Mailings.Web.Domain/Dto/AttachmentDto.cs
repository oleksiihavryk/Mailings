using System.ComponentModel.DataAnnotations.Schema;

namespace Mailings.Web.Domain.Dto;
public sealed class AttachmentDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string EncodedContent { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    [NotMapped]
    public byte[] BytesContent
    {
        get => Convert.FromBase64String(EncodedContent);
        set => EncodedContent = Convert.ToBase64String(value);
    }


    //Equals override
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is AttachmentDto other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(Id, Name, EncodedContent, ContentType);

    private bool Equals(AttachmentDto other)
        => Id.Equals(other.Id) && Name == other.Name &&
           EncodedContent == other.EncodedContent &&
           ContentType == other.ContentType;
}