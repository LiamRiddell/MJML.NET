using AngleSharp.Dom;
using MjmlDotNet.Core.Component;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-breakpoint/src/index.js
    public class MjmlBreakpointComponent : HeadComponent
    {
        public MjmlBreakpointComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "width", null }
            };
        }

        public override void Handler()
        {
            if (HasAttribute("width"))
                HtmlSkeleton.Breakpoint = GetAttribute("width");
        }
    }
}