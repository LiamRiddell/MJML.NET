using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    internal class MjmlTextComponent : BodyComponent
    {
        public MjmlTextComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlText;
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
                { Element.Html() }
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
                {TagHelpers.ConditionalTag($@"
                    <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                        <tr>
                            <td height=""{height}"" style=""vertical-align:top;height:{height};"">
                ")}

                {this.RenderContent()}

                {TagHelpers.ConditionalTag($@"
                            </td>
                        </tr>
                    </table>
                ")}
            ";
        }
    }
}