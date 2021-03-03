using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    internal class MjmlImageComponent : BodyComponent
    {
        public MjmlImageComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlImage;
        }

        public bool IsFullWidth()
        {
            return HasAttribute("full-width") ?
                GetAttribute("full-width").Equals("full-width", StringComparison.InvariantCultureIgnoreCase) :
                false;
        }

        public string GetContentWidth()
        {
            CssParsedUnit width = HasAttribute("width") ?
                CssUnitParser.Parse(GetAttribute("width")) :
                CssUnitParser.Parse($"{999999}px");

            return width.Value < GetContainerInnerWidth() ? width.ToString() : $"{GetContainerInnerWidth()}px";
        }

        public override void SetupStyles()
        {
            string width = GetContentWidth();
            bool isFullWidth = IsFullWidth();

            StyleLibraries.AddStyleLibrary("img", new Dictionary<string, string>() {
                { "border", GetAttribute("border") },
                { "border-left", GetAttribute("border-left") },
                { "border-right", GetAttribute("border-right") },
                { "border-top", GetAttribute("border-top") },
                { "border-bottom", GetAttribute("border-bottom") },
                { "border-radius", GetAttribute("border-radius") },
                { "display", "block" },
                { "outline", "none" },
                { "text-decoration", "none" },
                { "height", GetAttribute("height") },
                { "max-height", GetAttribute("max-height") },
                { "min-width", isFullWidth ? "100%" : null },
                { "width", "100%" },
                { "max-width", isFullWidth ? "100%" : null },
                { "font-size", GetAttribute("font-size") }
            });

            StyleLibraries.AddStyleLibrary("td", new Dictionary<string, string>() {
                { "width", isFullWidth ? null : width },
            });

            StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                { "min-width", isFullWidth ? "100%" : null },
                { "max-width", isFullWidth ? "100%" : null },
                { "width", isFullWidth ? width : null },
                { "border-collapse", "collapse"},
                { "border-spacing", "0px"}
            });
        }

        public override string HeadStyle()
        {
            return $@"
                @media only screen and (max-width:{HtmlSkeleton.Breakpoint}) {{
                  table.mj-full-width-mobile {{ width: 100% !important; }}
                  td.mj-full-width-mobile {{ width: auto !important; }}
                }}
            ";
        }

        public string RenderImage()
        {
            bool bHasHeight = HasAttribute("height");
            string height = GetAttribute("height");

            string img = $@"
                <img {HtmlAttributes(new Dictionary<string, string> {
                        { "alt", GetAttribute("alt") },
                        { "height", bHasHeight && height.Equals("auto", StringComparison.InvariantCultureIgnoreCase) ? height : CssUnitParser.Parse(height).Value.ToString() },
                        { "src", GetAttribute("src") },
                        { "srcset", GetAttribute("srcset") },
                        { "sizes", GetAttribute("sizes") },
                        { "style", "img" },
                        { "title", GetAttribute("title") },
                        { "width", GetContentWidth() },
                        { "usemap", GetAttribute("usemap") },
                     })}
                />
            ";

            if (HasAttribute("href"))
            {
                return $@"
                    <a {HtmlAttributes(new Dictionary<string, string>() {
                            { "href", GetAttribute("href") },
                            { "target", GetAttribute("target") },
                            { "rel", GetAttribute("rel") },
                            { "name", GetAttribute("name") },
                        })}
                    >
                    {img}
                    </a>
                ";
            }

            return img;
        }

        public override string RenderMjml()
        {
            bool bIsFluidMobile = HasAttribute("fluid-on-mobile");

            return $@"
            <table {HtmlAttributes(new Dictionary<string, string> {
                        { "border", "0" },
                        { "cellpadding", "0" },
                        { "cellspacing", "0" },
                        { "role", "presentation" },
                        { "style", "table" },
                        { "class", bIsFluidMobile ? "mj-full-width-mobile" : null },
                    })}
            >
                <tbody>
                    <tr>
                        <td {HtmlAttributes(new Dictionary<string, string> {
                                { "style", "td" },
                                { "class", bIsFluidMobile ? "mj-full-width-mobile" : null },
                            })}
                        >
                            {RenderImage()}
                        </td>
                    </tr>
                </tbody>
            </table>
            ";
        }
    }
}