using AngleSharp.Dom;
using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-preview/src/index.js
    public class MjmlPreviewComponent : HeadComponent
    {
        public MjmlPreviewComponent(Element element, BaseComponent parent) : base(element, parent)
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
            var content = GetContent();

            if (!string.IsNullOrWhiteSpace(content))
                HtmlSkeleton.PreviewText = content;
        }

        // LR: Omit the child components
        public override string RenderMjml()
        {
            this.Handler();
            return string.Empty;
        }
    }
}