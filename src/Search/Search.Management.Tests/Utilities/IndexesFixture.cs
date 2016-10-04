using Microsoft.Azure.Search.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;

namespace Search.Management.Tests.Utilities
{
    public class IndexesFixture : ResourceGroupFixture
    {
        public override void Initialize(MockContext context)
        {
            base.Initialize(context);
        }
    }
}
