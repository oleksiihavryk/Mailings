namespace Mailings.Resources.API.RawDto;
public class RawMailDto
{
    public Guid? Id { get; set; } = null;
    public string Theme { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    
}