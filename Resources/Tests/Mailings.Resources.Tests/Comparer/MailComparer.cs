using Mailings.Resources.Domain.Models;

namespace Mailings.Resources.Tests.Comparer;

internal class MailComparer : BaseComparer<Mail>, IMailComparer
{
    public MailComparer() 
        : base(Predicate)
    {
    }

    public override int GetHashCode(Mail obj)
    {
        int hashCode = 0;
        unchecked
        {
            hashCode = obj.Attachments?.GetHashCode() ?? -1 ^ obj.Content.GetHashCode()
                + obj.Id.GetHashCode() + obj.UserId.GetHashCode() * obj.Theme.GetHashCode();
        } 
        return hashCode;
    }

    private static bool Predicate(Mail x, Mail y)
    {
        var enumX = x.Attachments?.GetEnumerator();
        var enumY = y.Attachments?.GetEnumerator();
        
        if (enumX != null && enumY != null)
        {
            try
            {
                if (enumX.MoveNext() == true & enumY.MoveNext() == true)
                {
                    do
                    {
                        if (enumX.Current.Name != enumY.Current.Name ||
                            enumX.Current.Data != enumY.Current.Data ||
                            enumX.Current.ContentType != enumY.Current.ContentType)
                            return false;
                    }
                    while (enumX.MoveNext() & enumY.MoveNext());
                }
            }
            finally
            {
                enumX.Dispose();
                enumY.Dispose();
            }
        }
        else if (!object.ReferenceEquals(enumX, enumY))
            return false;

        return x.Id == y.Id &&
               x.Theme == y.Theme &&
               x.UserId == y.UserId &&
               x.Content == y.Content;
    }
}