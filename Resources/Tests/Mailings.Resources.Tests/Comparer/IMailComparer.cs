using System.Collections.Generic;
using Mailings.Resources.Domain.Models;

namespace Mailings.Resources.Tests.Comparer;

internal interface IMailComparer : IEqualityComparer<Mail>
{
}