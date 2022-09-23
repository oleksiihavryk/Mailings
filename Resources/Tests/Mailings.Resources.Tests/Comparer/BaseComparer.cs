using System;
using System.Collections.Generic;

namespace Mailings.Resources.Tests.Comparer;

internal abstract class BaseComparer<T> : IEqualityComparer<T>
{
    protected Func<T, T, bool> _predicate;

    protected BaseComparer(Func<T, T, bool> predicate)
    {
        _predicate = predicate;
    }

    public bool Equals(T? x, T? y)
    {
        if (object.ReferenceEquals(x, y)) return true;
        if (x == null || y == null) return false;
        if (GetHashCode(x) != GetHashCode(y)) return false;
        
        return _predicate(x, y);
    }
    public abstract int GetHashCode(T obj);

}