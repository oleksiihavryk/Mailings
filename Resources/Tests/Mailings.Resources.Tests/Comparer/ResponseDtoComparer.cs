using System;
using Mailings.Resources.Core.ResponseFactory;

namespace Mailings.Resources.Tests.Comparer;

internal class ResponseDtoComparer : BaseComparer<Response>, IResponseDtoComparer
{
    public ResponseDtoComparer()
        : base(Predicate)
    {
    }
    public ResponseDtoComparer(Func<Response, Response, bool> predicate)
        : base(predicate)
    {
    }

    public override int GetHashCode(Response obj)
    {
        unchecked
        {
            int hashCode;

            hashCode = (Convert.ToInt32(obj.IsSuccess) - 2) * obj.StatusCode;
            hashCode -= obj.Result != null ? obj.Result.GetHashCode() : 1;

            foreach (var m in obj.Messages)
                hashCode += m.GetHashCode();

            return hashCode;
        }
    }

    private static bool Predicate(
        Response x,
        Response y)
    {
        bool isEquals = x.IsSuccess == y.IsSuccess && 
                        x.StatusCode == y.StatusCode && 
                        Equals(x.Result, y.Result);

        var enumX = x.Messages.GetEnumerator();
        var enumY = y.Messages.GetEnumerator();
        try
        {
            if (enumX.MoveNext() == true & enumY.MoveNext() == true)
            {
                do
                {
                    if (!enumX.Current.Equals(enumY.Current))
                    {
                        isEquals = false;
                        break;
                    }
                }
                while (enumX.MoveNext() & enumY.MoveNext());
            }
        }
        finally
        {
            enumX.Dispose();
            enumY.Dispose();
        }

        return isEquals;
    }
}