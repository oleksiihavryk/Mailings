using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Shared.Comparers;
public sealed class MailComparer : IComparer<MailDto>
{
    private readonly Func<MailDto, MailDto, int> _predicate;

    public MailComparer(Func<MailDto, MailDto, int> predicate)
    {
        _predicate = predicate;
    }

    public int Compare(MailDto? x, MailDto? y)
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