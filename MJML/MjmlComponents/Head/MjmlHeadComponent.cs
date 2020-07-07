using Mjml.Core;
using Mjml.Core.Component;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head/src/index.js
    public class MjmlHeadComponent : HeadComponent
    {
        public MjmlHeadComponent(XElement element) : base(element)
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
        }
    }
}