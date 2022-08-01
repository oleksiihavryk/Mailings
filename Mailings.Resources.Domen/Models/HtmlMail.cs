using System.Text;

namespace Mailings.Resources.Domen.Models;

public class HtmlMail : Mail
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

    public HtmlMail(string theme, string userId)
        : this(theme, userId, Encoding.UTF8)
    {
    }
    public HtmlMail(string theme, string userId, Encoding encoding)
        : base(theme, userId)
    {
        _encoding = encoding;
    }
}