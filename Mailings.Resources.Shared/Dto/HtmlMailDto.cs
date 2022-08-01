using System.Text;

namespace Mailings.Resources.Shared.Dto;

public class HtmlMailDto : MailDto
{
    private readonly Encoding _encoding;

    public byte[] ByteContent { get; set; } = Array.Empty<byte>();
    public override string Content
    {
        get
        {
            string result = _encoding.GetString(ByteContent);
            return result;
        }
    }
    public Encoding Encoding => _encoding;

    public HtmlMailDto(string theme, string userId)
        : this(theme, userId, Encoding.UTF8)
    {
    }
    public HtmlMailDto(string theme, string userId, Encoding encoding)
        : base(theme, userId)
    {
        _encoding = encoding;
    }
}