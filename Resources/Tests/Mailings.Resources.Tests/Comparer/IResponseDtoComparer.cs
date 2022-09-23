using System.Collections.Generic;
using Mailings.Resources.Core.ResponseFactory;

namespace Mailings.Resources.Tests.Comparer;

internal interface IResponseDtoComparer : IEqualityComparer<Response>
{
}