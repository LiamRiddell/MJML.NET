using AngleSharp.Dom;
using MjmlDotNet.Components.Attributes;
using MjmlDotNet.Core.Components;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    internal class MjmlDividerComponent : BodyComponent
    {
        public MjmlDividerComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlDivider;
        }

        public override void SetupStyles()
        {
            // LR: Standard Styles
            StyleLibraries.AddStyleLibrary("p", new Dictionary<string, string>() {
                { "border-top", $"{GetAttribute("border-style")} {GetAttribute("border-width")} {GetAttribute("border-color")}" },
                { "font-family", GetAttribute("font-family") },
                { "font-size", "1px" },
                { "margin", "0px auto" },
                { "width", GetAttribute("width") },
            });

            // LR: Outlook requires specific width
            StyleLibraries.AddStyleLibrary("outlook", new Dictionary<string, string>() {
                { "border-top", $"{GetAttribute("border-style")} {GetAttribute("border-width")} {GetAttribute("border-color")}" },
                { "font-family", GetAttribute("font-family") },
                { "font-size", "1px" },
                { "margin", "0px auto" },
                { "width", GetOutlookWidth()},
            });
        }

        public string RenderAfter()
        {
            return $@"
                <!--[if mso | IE]>
                <table
                  {HtmlAttributes(new Dictionary<string, string> {
                    { "align", "center" },
                    { "border", "0" },
                    { "cellpadding", "0" },
                    { "cellspacing", "0" },
                    { "style", "outlook" },
                    { "role", "presentation" },
                    { "width", GetOutlookWidth() },
                  })}
                >
                  <tr>
                    <td style=""height:0;line-height:0;"">
                      &nbsp;
                    </td>
                  </tr>
                </table>
              <![endif]-->
            ";
        }

        public override string RenderMjml()
        {
            return $@"
                <p
                    {HtmlAttributes(new Dictionary<string, string> {
                        { "style", "p" }
                    })}>
                </p>

                { RenderAfter() }
            ";
        }
    }
}