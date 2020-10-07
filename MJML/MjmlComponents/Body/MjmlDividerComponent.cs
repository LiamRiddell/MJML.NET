using AngleSharp.Dom;
using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    public class MjmlDividerComponent : BodyComponent
    {
        public MjmlDividerComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "border-color", "#000000" },
                { "border-style", "solid" },
                { "border-width", "4px" },
                { "container-background-color", string.Empty },
                { "css-class", string.Empty },
                { "padding", "10px 25px" },
                { "padding-top", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "width", "100%" },
            };
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
                { "width", $"{GetContainerInnerWidth()}px" },
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
                    { "width", $"{GetContainerInnerWidth()}px" },
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