using System.ComponentModel.DataAnnotations.Schema;

namespace Mailings.Web.Shared.Dto;
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
}