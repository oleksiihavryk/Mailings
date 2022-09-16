using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Shared.Comparers;
public class MailComparer : IComparer<MailDto>
{
    protected readonly Func<MailDto, MailDto, int> _predicate;

    public MailComparer(Func<MailDto, MailDto, int> predicate)
    {
        _predicate = predicate;
    }

    public virtual int Compare(MailDto? x, MailDto? y)
    {
        if (x == null && y == null)
            return 0;
        else if (x == null && y != null)
            return 1;
        else if (x != null && y != null)
            return -1;
        else return _predicate(x, y);
    }
}