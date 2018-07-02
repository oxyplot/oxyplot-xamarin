using System;
using System.Collections.Generic;

namespace NewDemo
{
    public class SampleViewModel
    {
        public IEnumerable<int> Values => new[]
        {
            3, 5, 11, 12, 7
        };
    }
}
