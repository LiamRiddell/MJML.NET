using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlTextComponent : MjmlBodyComponent
    {
        public MjmlTextComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "color", "#000000" },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-style", string.Empty },
                { "font-weight", string.Empty },
                { "line-height", "1px" },
                { "letter-spacing", "none" },
                { "height", string.Empty },
                { "text-decoration", string.Empty },
                { "text-transform", string.Empty },
                { "align", "left" },
                { "container-background-color", string.Empty },
                { "padding", "10px 25px" },
                { "padding-top", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "css-class", string.Empty }
            };
        }

        public string RenderContent()
        {
            return $@"
            <div>
                { RenderChildren() }
            </div>
            ";
        }

        public override string RenderMjml()
        {
            return $@"{this.RenderContent()}";
        }
    }
}