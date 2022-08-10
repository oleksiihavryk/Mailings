using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mailings.Resources.Domain.MainModels;

public class HtmlMail : Mail
{
    private readonly Encoding _encoding = Encoding.UTF8;

    public byte[] ByteContent { get; set; } = Array.Empty<byte>();
    [NotMapped]
    public override string Content
    {
        get
        {
            string result = _encoding.GetString(ByteContent);
            return result;
        }
        set
        {
            var bytes = _encoding.GetBytes(value);
            ByteContent = bytes;
        }
    }
    [NotMapped]
    public Encoding Encoding => _encoding;

    public HtmlMail(string theme, string userId)
        : base(theme, userId)
    {
    }
}