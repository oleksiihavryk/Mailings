namespace Mailings.Web.API.ViewModels;
public class MailingViewModel
{
    public Guid Id { get; set; }
    public MailViewModel Mail { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> To { get; set; }
}