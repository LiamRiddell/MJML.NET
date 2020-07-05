using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents
{
    public class MjmlTextComponent : BodyComponent
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

        public override void SetupStyles()
        {
            // LR: Add styles
            StyleLibraries.AddStyleLibrary("text", new Dictionary<string, string>() {
                { "font-family", "15px" },
                { "font-size", "15px" },
                { "font-style", "15px" },
                { "font-weight", "15px" },
                { "letter-spacing", "15px" },
                { "line-height", "15px" },
                { "text-align", "15px" },
                { "text-decoration", "15px" },
                { "text-transform", "15px" },
                { "color", "15px" },
                { "height", "15px" },
            });
        }

        public string RenderContent()
        {
            return $@"
            <div {HtmlAttributes(new Dictionary<string, string> {
                { "style", "text" }
            })}>
                { RenderChildren() }
            </ div >
            ";
        }

        public override string RenderMjml()
        {
            return $@"{this.RenderContent()}";
        }
    }
}