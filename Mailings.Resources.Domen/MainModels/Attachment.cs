using System.ComponentModel.DataAnnotations.Schema;

namespace Mailings.Resources.Domain.MainModels;
public class Attachment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
    [NotMapped]
    public byte[] BytesData
    {
        get => Convert.FromBase64String(Data);
        set => Data = Convert.ToBase64String(value);
    }
    public string ContentType { get; set; } 
    public Mail Mail { get; set; } 
}