using System.Collections.Generic;
using Mailings.Resources.API.ResponseFactory;

namespace Mailings.Resources.Tests.Comparer;

internal interface IResponseDtoComparer : IEqualityComparer<Response>
{
}