using System.ComponentModel.DataAnnotations.Schema;

namespace Mailings.Web.Domain.ServicesModels;
/// <summary>
///     Mail attachment model
/// </summary>
public sealed class Attachment
{
    /// <summary>
    ///     Identifier of attachment
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;
    /// <summary>
    ///     Name of attachment
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    ///     Encoded content in string by encoding Base64
    /// </summary>
    public string EncodedContent { get; set; } = string.Empty;
    /// <summary>
    ///     Content type of attachment
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    /// <summary>
    ///     Byte content of attachment
    /// </summary>
    [NotMapped]
    public byte[] BytesContent
    {
        get => Convert.FromBase64String(EncodedContent);
        set => EncodedContent = Convert.ToBase64String(value);
    }


    //Equals override
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || obj is Attachment other && Equals(other);
    public override int GetHashCode()
        => HashCode.Combine(Id, Name, EncodedContent, ContentType);

    /// <summary>
    ///     Equal operation between attachments 
    /// </summary>
    /// <param name="other">
    ///     Other attachment model
    /// </param>
    /// <returns>
    ///     Is equal current comparing objects
    /// </returns>
    private bool Equals(Attachment other)
        => Id.Equals(other.Id) && Name == other.Name &&
           EncodedContent == other.EncodedContent &&
           ContentType == other.ContentType;
}