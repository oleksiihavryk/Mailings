using System.ComponentModel.DataAnnotations.Schema;

namespace Mailings.Resources.API.Dto;
public class AttachmentDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; }
    public string EncodedContent { get; set; }
    public string ContentType { get; set; }
    [NotMapped]
    public byte[] BytesContent
    {
        get => Convert.FromBase64String(EncodedContent);
        set => EncodedContent = Convert.ToBase64String(value);
    }
}