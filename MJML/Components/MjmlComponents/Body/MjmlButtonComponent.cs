using AngleSharp.Dom;
using MjmlDotNet.Core.Component;
using MjmlDotNet.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    public class MjmlButtonComponent : BodyComponent
    {
        public MjmlButtonComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "align", "center" },
                { "background-color", "#414141" },
                { "border-bottom", null },
                { "border-left", null },
                { "border-radius", "3px" },
                { "border-right", null },
                { "border-top", null },
                { "border", "none" },
                { "color", "#ffffff" },
                { "container-background-color", null },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-style", null },
                { "font-weight", "normal" },
                { "height", null },
                { "href", null },
                { "name", null },
                { "inner-padding", "10px 25px" },
                { "letter-spacing", null },
                { "line-height", "120%" },
                { "padding-bottom", null },
                { "padding-left", null },
                { "padding-right", null },
                { "padding-top", null },
                { "padding", "10px 25px" },
                { "rel", null },
                { "target", null },
                { "text-decoration", "none" },
                { "text-transform", "none" },
                { "vertical-align", "middle" },
                { "text-align", "center" },
                { "width", null },
            };
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                { "border-collapse", "separate" },
                { "width", GetAttribute("width") },
                { "line-height", "100%" },
            });

            StyleLibraries.AddStyleLibrary("td", new Dictionary<string, string>() {
                { "border", GetAttribute("border") },
                { "border-bottom", GetAttribute("border-bottom") },
                { "border-left", GetAttribute("border-left") },
                { "border-radius", GetAttribute("border-radius") },
                { "border-right", GetAttribute("border-right") },
                { "border-top", GetAttribute("border-top") },
                { "cursor", "auto" },
                { "font-style", GetAttribute("font-style") },
                { "height", GetAttribute("height") },
                { "mso-padding-alt", GetAttribute("inner-padding") },
                { "text-align", GetAttribute("text-align") },
                { "background", GetAttribute("background-color") },
            });

            StyleLibraries.AddStyleLibrary("content", new Dictionary<string, string>() {
                { "display", "inline-block" },
                { "width", CalculateAWidth(GetAttribute("width")) }, // TODO: this.calculateAWidth(this.getAttribute('width'))
                { "background", GetAttribute("background-color") },
                { "color", GetAttribute("color") },
                { "font-family", GetAttribute("font-family") },
                { "font-size", GetAttribute("font-size") },
                { "font-style", GetAttribute("font-style") },
                { "font-weight", GetAttribute("font-weight") },
                { "line-height", GetAttribute("line-height") },
                { "letter-spacing", GetAttribute("letter-spacing") },
                { "margin", "0" },
                { "text-decoration", GetAttribute("text-decoration") },
                { "text-transform", GetAttribute("tex-transform") },
                { "padding", GetAttribute("inner-padding") },
                { "mso-padding-alt", "0px" },
                { "text-align", GetAttribute("text-align") },
                { "border-radius", GetAttribute("border-radius") },
            });
        }

        public string CalculateAWidth(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            CssParsedUnit parsedWidth = CssUnitParser.Parse(content);

            if (!parsedWidth.Unit.Equals("px"))
                return null;

            var borders = CssBoxModel.BorderWidth;

            var innerPaddings =
                GetShorthandAttributeValue("inner-padding", "left") +
                GetShorthandAttributeValue("inner-padding", "right");

            return $"{parsedWidth.Value - innerPaddings - borders}px";
        }

        public override string RenderMjml()
        {
            var tag = HasAttribute("href") ? "a" : "p";

            return $@"
                <table {HtmlAttributes(new Dictionary<string, string> {
                            { "border", "0"},
                            { "cellpadding", "0"},
                            { "cellspacing", "0"},
                            { "role", "presentation"},
                            { "style", "table"}
                       })}
                >
                    <tr>
                        <td {HtmlAttributes(new Dictionary<string, string> {
                                { "align", "center"},
                                { "bgcolor", GetAttribute("background-color")},
                                { "role", "presentation"},
                                { "style", "td"},
                                { "valign", GetAttribute("vertical-align") }
                           })}
                        >
                            <{tag} {HtmlAttributes(new Dictionary<string, string> {
                                        { "href", GetAttribute("href")},
                                        { "rel", GetAttribute("rel")},
                                        { "name", GetAttribute("name")},
                                        { "style", "content"},
                                        { "target", tag.Equals("a") ? GetAttribute("target") : null }
                                   })}
                            >
                                {RenderChildren()}
                            </{tag}>
                        </td>
                    </tr>
                </table>
            ";
        }
    }
}