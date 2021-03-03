using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-title/src/index.js
    internal class MjmlTitleComponent : HeadComponent
    {
        public MjmlTitleComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.NoAttributes;
        }

        public override void Handler()
        {
            var content = GetContent();

            if (!string.IsNullOrWhiteSpace(content))
                HtmlSkeleton.Title = content;
        }

        public override string RenderMjml()
        {
            // LR: Run the handler instead.
            Handler();

            // LR: Omit the child components
            return string.Empty;
        }
    }
}