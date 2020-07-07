using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-font/src/index.js
    public class MjmlFontComponent : HeadComponent
    {
        public MjmlFontComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "name", null },
                { "href", null },
            };
        }

        public override void Handler()
        {
            if (HasAttribute("name") && HasAttribute("href"))
                HtmlSkeleton.AddFont(GetAttribute("name"), GetAttribute("href"));
        }
    }
}