using Mailings.Web.Domain.ServicesModels;

namespace Mailings.Web.Shared.Comparers;
/// <summary>
///     Mail comparer
/// </summary>
public class MailComparer : IComparer<Mail>
{
    /// <summary>
    ///     Mail comparing predicate 
    /// </summary>
    protected readonly Func<Mail, Mail, int> _predicate;

    public MailComparer(Func<Mail, Mail, int> predicate)
    {
        _predicate = predicate;
    }

    /// <summary>
    ///     Compare mails
    /// </summary>
    /// <param name="x">
    ///     First element
    /// </param>
    /// <param name="y">
    ///     Second element
    /// </param>
    /// <returns>
    ///     Number of comparing
    /// </returns>
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