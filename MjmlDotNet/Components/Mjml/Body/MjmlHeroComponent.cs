using AngleSharp.Dom;
using MjmlDotNet.Components.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Components.Mjml.Body
{
    internal class MjmlHeroComponent : BodyComponent
    {
        public MjmlHeroComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlHero;
        }

        public string GetBackground()
        {
            string backgroundColor = GetAttribute("background-color");
            bool hasBackgroundUrl = Element.HasAttribute("background-url");

            return $"{backgroundColor} {(hasBackgroundUrl ? $"url({GetAttribute("background-url")}) no-repeat {GetAttribute("background-position")} / cover" : string.Empty)}";
        }

        public override void SetupStyles()
        {
            var backgroundHeight = CssUnitParser.Parse(GetAttribute("background-height"));
            var backgroundWidth = CssUnitParser.Parse(GetAttribute("background-width"));
            var backgroundRatio = Math.Round(backgroundHeight.Value / backgroundWidth.Value * 100d);
            var width = Element.HasAttribute("background-width") ? GetAttribute("background-width") : $"{GetContainerInnerWidth()}px";

            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "margin", "0 auto" },
                { "max-width", $"{GetContainerInnerWidth()}px" }
            });

            StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                { "width", "100%" }
            });

            StyleLibraries.AddStyleLibrary("tr", new Dictionary<string, string>() {
                { "vertical-align", "top" }
            });

            StyleLibraries.AddStyleLibrary("td-fluid", new Dictionary<string, string>() {
                { "width", "0.01%" },
                { "padding-bottom", $"{backgroundRatio}%" },
                { "mso-padding-bottom-alt", "0" }
            });

            StyleLibraries.AddStyleLibrary("hero", new Dictionary<string, string>() {
                { "background", GetBackground() },
                { "background-position", GetAttribute("background-position") },
                { "background-repeat", "no-repeat" },
                { "padding", GetAttribute("padding") },
                { "padding-top", GetAttribute("padding-top") },
                { "padding-left", GetAttribute("padding-left") },
                { "padding-right", GetAttribute("padding-right") },
                { "padding-bottom", GetAttribute("padding-bottom") },
                { "vertical-align", GetAttribute("vertical-align") }
            });

            StyleLibraries.AddStyleLibrary("outlook-table", new Dictionary<string, string>() {
                { "width", $"{GetContainerInnerWidth()}px" }
            });

            StyleLibraries.AddStyleLibrary("outlook-td", new Dictionary<string, string>() {
                { "line-height", "0" },
                { "font-size", "0a" },
                { "mso-line-height-rule", "exactly" },
            });

            StyleLibraries.AddStyleLibrary("outlook-inner-table", new Dictionary<string, string>() {
                { "width", $"{GetContainerInnerWidth()}px" }
            });

            StyleLibraries.AddStyleLibrary("outlook-image", new Dictionary<string, string>() {
                { "border", "0" },
                { "height", GetAttribute("background-height") },
                { "mso-position-horizontal", "center" },
                { "position", "absolute" },
                { "top", "0" },
                { "width", width },
                { "z-index", "-3" }
            });

            StyleLibraries.AddStyleLibrary("outlook-inner-td", new Dictionary<string, string>() {
                { "background-color", GetAttribute("inner-background-color") },
                { "padding", GetAttribute("padding") },
                { "padding-top", GetAttribute("padding-top") },
                { "padding-left", GetAttribute("padding-left") },
                { "padding-right", GetAttribute("padding-right") },
                { "padding-bottom", GetAttribute("padding-bottom") },
            });

            StyleLibraries.AddStyleLibrary("inner-table", new Dictionary<string, string>() {
                { "width", "100%" },
                { "margin", "0px" }
            });

            StyleLibraries.AddStyleLibrary("inner-div", new Dictionary<string, string>() {
                { "background-color", GetAttribute("inner-background-color") },
                { "float", GetAttribute("align") },
                { "margin", "0px auto" },
                { "width", GetAttribute("width") }
            });
        }

        public override string RenderChildren()
        {
            if (!Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var childComponent in Children)
            {
                string childContent = childComponent.RenderMjml();

                if (string.IsNullOrWhiteSpace(childContent))
                    continue;

                if (childComponent.IsRawElement())
                {
                    sb.Append(childContent);
                }
                else
                {
                    var bodyComponent = childComponent as BodyComponent;

                    sb.Append($@"
                        <tr>
                            <td {bodyComponent.HtmlAttributes(new Dictionary<string, string>() {
                                { "align", bodyComponent.GetAttribute("align") },
                                { "background", bodyComponent.GetAttribute("container-background-color") },
                                { "class", bodyComponent.GetAttribute("css-class") },
                                { "style", InlineCss(new Dictionary<string, string>() {
                                    { "background", bodyComponent.GetAttribute("container-background-color") },
                                    { "font-size", "0px" },
                                    { "padding", bodyComponent.GetAttribute("padding") },
                                    { "padding-top", bodyComponent.GetAttribute("padding-top") },
                                    { "padding-right", bodyComponent.GetAttribute("padding-right") },
                                    { "padding-bottom", bodyComponent.GetAttribute("padding-bottom") },
                                    { "padding-left", bodyComponent.GetAttribute("padding-left") },
                                    { "word-break", "break-word" }
                                })}
                            })}>
                                {childContent}
                           </td>
                        </tr>
                    ");
                }
            }

            return sb.ToString();
        }

        private object RenderContent()
        {
            return $@"
                <!--[if mso | IE]>
                <table {HtmlAttributes(new Dictionary<string, string> {
                        { "align", GetAttribute("align") },
                        { "border", "0" },
                        { "cellpadding", "0" },
                        { "cellspacing", "0" },
                        { "role", "presentation" },
                        { "style", "outlook-inner-table" },
                        { "width", $"{GetContainerInnerWidth()}px" }
                })}>
                    <tr>
                        <td {HtmlAttributes(new Dictionary<string, string> {
                            { "style", "outlook-inner-td" }
                        })}>
            <![endif]-->
                            <div {HtmlAttributes(new Dictionary<string, string> {
                                { "align", GetAttribute("align") },
                                { "class", GetAttribute("mj-hero-content") },
                                { "style", "inner-div" }
                            })}>
                                <table {HtmlAttributes(new Dictionary<string, string> {
                                    { "border", "0" },
                                    { "cellpadding", "0" },
                                    { "cellspacing", "0" },
                                    { "role", "presentation" },
                                    { "style", "inner-table" }
                                })}>
                                    <tr>
                                        <td {HtmlAttributes(new Dictionary<string, string> {
                                            { "style", "inner-td" }
                                        })}>
                                            <table {HtmlAttributes(new Dictionary<string, string> {
                                                { "border", "0" },
                                                { "cellpadding", "0" },
                                                { "cellspacing", "0" },
                                                { "role", "presentation" },
                                                { "style", "inner-table" }
                                            })}>
                                                {RenderChildren()}
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
            <!--[if mso | IE]>
                        </td>
                    </tr>
                </td>
            <![endif]-->
            ";
        }

        private object RenderMode()
        {
            string background = GetAttribute("background-url");

            switch (GetAttribute("mode"))
            {
                case "fluid-height":
                    string magicTd = HtmlAttributes(new Dictionary<string, string> {
                            { "style", "td-fluid" },
                    });

                    return $@"
                        <td {magicTd} />
                        <td {HtmlAttributes(new Dictionary<string, string> {
                                    { "background", background },
                                    { "style", "hero" }
                                })}
                        >
                           {RenderContent()}
                        </td>
                        <td {magicTd} />
                    ";

                default:
                    var heightCss = CssUnitParser.Parse(GetAttribute("height"));
                    var paddingTop = GetShorthandAttributeValue("padding", "top");
                    var paddingBottom = GetShorthandAttributeValue("padding", "bottom");

                    // LR: Start with the height value. e.g. 500px
                    var height = heightCss.Value;

                    // LR: Convert % to PX
                    if (heightCss.Unit.Equals("%")) height = GetContainerInnerWidth() / 100 * height;

                    // LR: Remove the top and bottom padding from the height e.g. 500px - 24px - 24px = 452px
                    height -= paddingTop + paddingBottom;

                    return $@"
                        <td {HtmlAttributes(new Dictionary<string, string> {
                                { "background", background },
                                { "style", "hero" },
                                { "height", $"{height}" }
                            })}
                        >
                           {RenderContent()}
                        </td>
                    ";
            }
        }

        public override string RenderMjml()
        {
            return $@"
            <!--[if mso | IE]>
                <table {HtmlAttributes(new Dictionary<string, string> {
                            { "align", "center" },
                            { "border", "0" },
                            { "cellpadding", "0" },
                            { "cellspacing", "0" },
                            { "role", "presentation" },
                            { "style", "outlook-table" },
                            { "class", $"{GetContainerInnerWidth()}px" }
                        })}
                >
                    <tr>
                        <td {HtmlAttributes(new Dictionary<string, string> {
                                { "style", "outlook-td" }
                            })}
                        >
                            <v:image {HtmlAttributes(new Dictionary<string, string> {
                                    { "style", "outlook-image" },
                                    { "src", GetAttribute("background-url") },
                                    { "xmlns:v", "urn:schemas-microsoft-com:vml" }
                                })}
                            />
            <![endif]-->
                            <div {HtmlAttributes(new Dictionary<string, string> {
                                        { "align", GetAttribute("align") },
                                        { "class", GetAttribute("css-class") },
                                        { "style", "div" }
                                    })}
                            >
                                <table {HtmlAttributes(new Dictionary<string, string> {
                                            { "border", "0" },
                                            { "cellpadding", "0" },
                                            { "cellspacing", "0" },
                                            { "role", "presentation" },
                                            { "style", "table" }
                                        })}
                                >
                                    <tr {HtmlAttributes(new Dictionary<string, string> {
                                            { "style", "tr" }
                                        })}
                                    >
                                       {RenderMode()}
                                    </tr>
                                </table>
                            </div>
            <!--[if mso | IE]>
                        </td>
                    </tr>
                </table>
            <![endif]-->
            ";
        }
    }
}