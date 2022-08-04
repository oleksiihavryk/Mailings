using System.Collections.Generic;
using Mailings.Resources.API.ResponseFactory;

namespace Mailings.Resources.Tests.Comparer;
public interface IResponseDtoComparer : IEqualityComparer<Response>
{
}