using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-style/src/index.js
    internal class MjmlStyleComponent : HeadComponent
    {
        public MjmlStyleComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Head.MjmlStyle;
        }

        public override void Handler()
        {
            string css = GetContent();

            if (string.IsNullOrWhiteSpace(css))
                return;

            HtmlSkeleton.AddStyle(css, HasAttribute("inline"));
        }

        // LR: Omit the child components
        public override string RenderMjml()
        {
            this.Handler();
            return string.Empty;
        }
    }
}