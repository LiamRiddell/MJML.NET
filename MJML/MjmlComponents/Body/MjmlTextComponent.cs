using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
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
                { "font-family", GetAttribute("font-family") },
                { "font-size", GetAttribute("font-size") },
                { "font-style", GetAttribute("font-style") },
                { "font-weight", GetAttribute("font-weight") },
                { "letter-spacing", GetAttribute("letter-spacing") },
                { "line-height", GetAttribute("line-height") },
                { "text-align", GetAttribute("align") },
                { "text-decoration", GetAttribute("text-decoration") },
                { "text-transform", GetAttribute("text-transform") },
                { "color", GetAttribute("color") },
                { "height", GetAttribute("height") },
            });
        }

        public string RenderContent()
        {
            return $@"
            <div {HtmlAttributes(new Dictionary<string, string> {
                { "style", "text" }
            })}>
                { RenderChildren() }
            </div>
            ";
        }

        public override string RenderMjml()
        {
            var height = GetAttribute("height");

            if (string.IsNullOrWhiteSpace(height))
            {
                return this.RenderContent();
            }

            return $@"
                {TagHelpers.ConditionalTag($@"<table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td height=""{height}"" style=""vertical-align:top;height:{height};"">")}
                    {this.RenderContent()}
                {TagHelpers.ConditionalTag("</td></tr></table>")}
            ";
        }
    }
}