using AngleSharp.Dom;
using Mjml.Core.Component;
using Mjml.HtmlComponents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    public class MjmlHeroComponent : BodyComponent
    {
        public MjmlHeroComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "background-color", "#FFFFFF" },
                { "background-height", null },
                { "background-position", "center center" },
                { "background-url", null },
                { "background-width", null },
                { "css-class", null },
                { "height", "0px" },
                { "mode", "fluid-height" },
                { "padding", "0px" },
                { "padding-bottom", "0px" },
                { "padding-left", "0px" },
                { "padding-right", "0px" },
                { "padding-top", "0px" },
                { "vertical-align", "top" },
                { "width", null },
            };
        }

        public override void SetupStyles()
        {
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
                { "padding-bottom", $"{GetContainerInnerWidth()}px" }, // `${backgroundRatio}%`
                { "mso-padding-bottom-alt", "0" }
            });

            StyleLibraries.AddStyleLibrary("hero", new Dictionary<string, string>() {
                { "background", null }, // TODO
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
                { "width", null }, // TODO
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

        public override string RenderMjml()
        {
            return ""; // TODO;
        }
    }
}