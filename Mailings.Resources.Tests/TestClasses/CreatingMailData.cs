using System;
using System.Collections;
using System.Collections.Generic;

namespace Mailings.Resources.Tests.TestClasses;

public class CreatingMailData : IEnumerable<object[]>
{
    public IList<object[]> Objects =>
        new List<object[]>
        {
            new object[] { "vas", "vju192", "<h1>vdjoivshdiurbfav</h1>" },
            new object[] { "vdasklvndai", null, "<p>s.ajfnoinvja</p>" },
            new object[] { "vas", "", "vdjoivshdiurbfav" },
            new object[] { "    ", "dsf", "fjifsako" }
        };

    public IEnumerator<object[]> GetEnumerator()
        => Objects.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}