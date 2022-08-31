namespace Mailings.Web.API.ViewModels;
public class CreatedMailingsViewModel
{
    public Guid Id { get; set; }
    public Guid MailId { get; set; }
    public string Name { get; set; }
    public List<string> To { get; set; }
}