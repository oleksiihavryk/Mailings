using System.Collections.Generic;
using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Tests.Comparer;

internal interface IMailComparer : IEqualityComparer<Mail>
{
}