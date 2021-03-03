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
    internal class MjmlNavbarLinkComponent : BodyComponent
    {
        public MjmlNavbarLinkComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlNavbarLink;
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("a", new Dictionary<string, string>() {
                { "display", "inline-block" },
                { "color", GetAttribute("color") },
                { "font-family", GetAttribute("font-family") },
                { "font-size", GetAttribute("font-size") },
                { "font-style", GetAttribute("font-style") },
                { "font-weight", GetAttribute("font-weight") },
                { "letter-spacing", GetAttribute("letter-spacing") },
                { "line-height", GetAttribute("line-height") },
                { "text-decoration", GetAttribute("text-decoration") },
                { "text-transform", GetAttribute("text-transform") },
                { "padding", GetAttribute("padding") },
                { "padding-top", GetAttribute("padding-top") },
                { "padding-left", GetAttribute("padding-left") },
                { "padding-right", GetAttribute("padding-right") },
                { "padding-bottom", GetAttribute("padding-bottom") }
            });

            StyleLibraries.AddStyleLibrary("td", new Dictionary<string, string>() {
                { "padding", GetAttribute("padding") },
                { "padding-top", GetAttribute("padding-top") },
                { "padding-left", GetAttribute("padding-left") },
                { "padding-right", GetAttribute("padding-right") },
                { "padding-bottom", GetAttribute("padding-bottom") }
            });
        }

        public string RenderContent()
        {
            var href = GetAttribute("href");
            var navbarBaseUrl = GetAttribute("navbarBaseUrl");
            var link = !string.IsNullOrEmpty(navbarBaseUrl) ? $"{navbarBaseUrl}{href}" : href;

            var cssClass = HasAttribute("css-class") ? GetAttribute("css-class") : string.Empty;

            return $@"
                <a {HtmlAttributes(new Dictionary<string, string> {
                    { "class", $"mj-link{cssClass}" },
                    { "href", link },
                    { "rel", GetAttribute("rel") },
                    { "target", GetAttribute("target") },
                    { "name", GetAttribute("name") },
                    { "style", "a" }
                })}>
                    {GetContent()}
                </a>
            ";
        }

        public override string RenderMjml()
        {
            return $@"
                {TagHelpers.ConditionalTag($@"
                    <td {HtmlAttributes(new Dictionary<string, string> {
                        { "style", "td" },
                        { "class", CssHelper.SuffixCssClasses(GetAttribute("css-class"), "outlook") }
                    })}>
                ")}

                {RenderContent()}

                {TagHelpers.ConditionalTag($@"
                    </td>
                ")}
            ";
        }
    }
}