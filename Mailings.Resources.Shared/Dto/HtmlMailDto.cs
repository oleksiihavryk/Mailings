using System.Text;

namespace Mailings.Resources.Shared.Dto;

public class HtmlMailDto : MailDto
{
    private readonly Encoding _encoding = Encoding.UTF8;

    public byte[] ByteContent { get; set; } = Array.Empty<byte>();
    public override string Content
    {
        get
        {
            string result = _encoding.GetString(ByteContent);
            return result;
        }
    }

    public HtmlMailDto(string theme, string userId)
        : base(theme, userId)
    {
    }
}