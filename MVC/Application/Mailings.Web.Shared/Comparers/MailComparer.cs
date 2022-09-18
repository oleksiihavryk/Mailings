using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Shared.Comparers;
public class MailComparer : IComparer<Mail>
{
    protected readonly Func<Mail, Mail, int> _predicate;

    public MailComparer(Func<Mail, Mail, int> predicate)
    {
        _predicate = predicate;
    }

    public virtual int Compare(Mail? x, Mail? y)
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