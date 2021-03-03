using AngleSharp.Dom;
using MjmlDotNet.Components.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/master/packages/mjml-navbar/src/Navbar.js
    internal class MjmlNavbarComponent : BodyComponent
    {
        public MjmlNavbarComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlNavbar;
        }

        public override string HeadStyle()
        {
            return $@"
                noinput.mj-menu-checkbox {{ display:block!important; max-height:none!important; visibility:visible!important; }}

                @media only screen and (max-width: {HtmlSkeleton.Breakpoint}) {{
                    .mj-menu-checkbox[type=""checkbox""] ~ .mj-inline-links {{ display:none!important; }}
                    .mj-menu-checkbox[type=""checkbox""]:checked ~ .mj-inline-links,
                    .mj-menu-checkbox[type=""checkbox""] ~ .mj-menu-trigger {{ display:block!important; max-width:none!important; max-height:none!important; font-size:inherit!important; }}
                    .mj-menu-checkbox[type=""checkbox""] ~ .mj-inline-links > a {{ display:block!important; }}
                    .mj-menu-checkbox[type=""checkbox""]:checked ~ .mj-menu-trigger .mj-menu-icon-close {{ display:block!important; }}
                    .mj-menu-checkbox[type=""checkbox""]:checked ~ .mj-menu-trigger .mj-menu-icon-open {{ display:none!important; }}
                }}
            ";
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "align", GetAttribute("align") },
                { "width", GetAttribute("100%") }
            });

            StyleLibraries.AddStyleLibrary("label", new Dictionary<string, string>() {
                { "display", "block" },
                { "cursor", "pointer" },
                { "mso-hide", "all" },
                { "-moz-user-select", "none" },
                { "user-select", "none" },
                { "color", GetAttribute("ico-color") },
                { "font-size", GetAttribute("ico-font-size") },
                { "font-family", GetAttribute("ico-font-family") },
                { "text-transform", GetAttribute("ico-text-transform") },
                { "text-decoration", GetAttribute("ico-text-decoration") },
                { "line-height", GetAttribute("ico-line-height") },
                { "padding-top", GetAttribute("ico-padding-top") },
                { "padding-right", GetAttribute("ico-padding-right") },
                { "padding-bottom", GetAttribute("ico-padding-bottom") },
                { "padding-left", GetAttribute("ico-padding-left") },
                { "padding", GetAttribute("ico-padding") }
            });

            StyleLibraries.AddStyleLibrary("trigger", new Dictionary<string, string>() {
                { "display", "none" },
                { "max-height", "0px" },
                { "max-width", "0px" },
                { "font-size", "0px" },
                { "overflow", "hidden" }
            });

            StyleLibraries.AddStyleLibrary("icoOpen", new Dictionary<string, string>() {
                { "mso-hide", "all" }
            });

            StyleLibraries.AddStyleLibrary("icoClose", new Dictionary<string, string>() {
                { "display", "none" },
                { "mso-hide", "all" }
            });
        }

        public string RenderHamburger()
        {
            var key = TagHelpers.GetRandomHexNumber(8);

            return $@"
                {TagHelpers.MsoConditionalTag(
                   $@"<input type=""checkbox"" id=""{key}"" class=""mj-menu-checkbox"" style=""display:none !important; max-height:0; visibility:hidden;"" />",
                   true
                )}
                <div {HtmlAttributes(new Dictionary<string, string> {
                    { "class", "mj-menu-trigger" },
                    { "style", "trigger" }
                })}>
                    <label {HtmlAttributes(new Dictionary<string, string> {
                        { "for", key },
                        { "class", "mj-menu-label" },
                        { "style", "label" },
                        { "align", GetAttribute("ico-align") }
                    })}>
                        <span {HtmlAttributes(new Dictionary<string, string> {
                            { "class", "mj-menu-icon-open" },
                            { "style", "icoOpen" }
                        })}>
                            {GetAttribute("ico-open")}
                        </span>

                        <span {HtmlAttributes(new Dictionary<string, string> {
                            { "class", "mj-menu-icon-close" },
                            { "style", "icoClose" }
                        })}>
                            {GetAttribute("ico-close")}
                        </span>
                    </label>
                </div>
            ";
        }

        public override string RenderChildren()
        {
            if (!Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var childComponent in Children)
            {
                childComponent.AddAttributeRaw("navbarBaseUrl", GetAttribute("base-url"));

                string childContent = childComponent.RenderMjml();

                if (string.IsNullOrWhiteSpace(childContent))
                    continue;

                sb.Append(childContent);
            }

            return sb.ToString();
        }

        public override string RenderMjml()
        {
            return $@"
                {(GetAttribute("hamburger").Equals("hamburger", System.StringComparison.InvariantCultureIgnoreCase) ? RenderHamburger() : string.Empty )}

                <div {HtmlAttributes(new Dictionary<string, string> {
                    { "class", "mj-inline-links" },
                    { "style", "div" }
                })}>
                    {TagHelpers.ConditionalTag($@"
                        <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""{GetAttribute("align")}"">
                            <tr>
                    ")}
                                
                    {RenderChildren()}

                    {TagHelpers.ConditionalTag($@"
                            </tr>
                        </table>
                    ")}
                </div>
            ";
        }
    }
}