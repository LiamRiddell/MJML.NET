using AngleSharp.Dom;
using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-style/src/index.js
    public class MjmlStyleComponent : HeadComponent
    {
        public MjmlStyleComponent(Element element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "inline", null }
            };
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