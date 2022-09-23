using System.ComponentModel.DataAnnotations.Schema;

namespace Mailings.Resources.Domain.Models;

public class TextMail : Mail
{
    public string StringContent { get; set; } = string.Empty;
    [NotMapped]
    public override string Content
    {
        get => StringContent; 
        set => StringContent = value; 
    }

    public TextMail(string theme, string userId) 
        : base(theme, userId)
    {
    }
}