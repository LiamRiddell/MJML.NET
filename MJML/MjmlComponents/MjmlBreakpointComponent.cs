using Mjml.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents
{
    public class MjmlBreakpointComponent : HeadComponent
    {
        public MjmlBreakpointComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
            };
        }

        public override void Handler()
        {
            Console.WriteLine("<mj-breakpoint> handler");
        }
    }
}