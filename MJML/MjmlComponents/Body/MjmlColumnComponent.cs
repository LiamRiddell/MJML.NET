using Mjml.Core.Component;
using Mjml.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-column/src/index.js
    public class MjmlColumnComponent : BodyComponent
    {
        public string ContainerWidth { get; set; } = null;
        public int ParentSectionColumnCount { get; set; }

        public MjmlColumnComponent(XElement element) : base(element)
        {
        }

        public string GetContainerWidth()
        {
            if (!string.IsNullOrWhiteSpace(ContainerWidth))
                return ContainerWidth;

            float innerBorders =
                GetShorthandAttributeValue("inner-border", "left") +
                GetShorthandAttributeValue("inner-border", "right");

            float allPaddings =
                CssBoxModel.PaddingWidth +
                CssBoxModel.BorderWidth +
                innerBorders;

            // NOTE: This will include our HACK for text elements
            int nonRawSiblings = GetSectionColumnCount();

            ParentSectionColumnCount = nonRawSiblings;

            // LR: Current container width (inherited from mj-body)
            CssParsedUnit parentWidth = CssUnitParser.Parse(HtmlSkeleton.ContainerWidth);

            // LR: Calculate the ContainerWidth for this column
            if (HasAttribute("width"))
                ContainerWidth = GetAttribute("width");
            else
                ContainerWidth = $"{parentWidth.Value / nonRawSiblings}px";

            CssParsedUnit parsedWidth = CssUnitParser.Parse(ContainerWidth);

            // LR: Handle Percentage values
            if (parsedWidth.Unit.Equals("%", StringComparison.InvariantCultureIgnoreCase))
            {
                ContainerWidth = $"{(parentWidth.Value * parsedWidth.Value / 100) - allPaddings}";
            }
            else
            {
                ContainerWidth = $"{parsedWidth.Value - allPaddings}px";
            }

            return ContainerWidth;
        }

        public int GetSectionColumnCount()
        {
            return Element.Parent
                .Elements()
                .Count(n => n.NodeType.Equals(XmlNodeType.Element));
        }

        public bool HasGutter()
        {
            return HasAttribute("padding") ||
                   HasAttribute("padding-bottom") ||
                   HasAttribute("padding-left") ||
                   HasAttribute("padding-right") ||
                   HasAttribute("padding-top");
        }

        public CssParsedUnit GetParsedWidth()
        {
            string width = string.Empty;

            if (HasAttribute("width"))
                width = GetAttribute("width");
            else
                width = $"{100 / GetSectionColumnCount()}%";

            CssParsedUnit parsedWidth = CssUnitParser.Parse(width);

            return parsedWidth;
        }

        public string GetMobileWidth()
        {
            var width = GetAttribute("width");
            var mobileWidth = GetAttribute("mobileWidth");

            if (string.IsNullOrWhiteSpace(mobileWidth))
                return "100%";

            if (string.IsNullOrWhiteSpace(width))
                return $"{100 / ParentSectionColumnCount}%";

            var parsedWidth = CssUnitParser.Parse(width);

            switch (parsedWidth.Unit.ToLower())
            {
                case "%":
                    return width;

                case "px":
                default:
                    return $"{parsedWidth.Value / CssUnitParser.Parse(ContainerWidth).Value}%";
            }
        }

        public string GetWidthAsPixel()
        {
            var parsedWidth = GetParsedWidth();
            var parsedContainerWidth = CssUnitParser.Parse(GetContainerWidth());

            if (parsedWidth.Unit.Equals("%", StringComparison.InvariantCultureIgnoreCase))
                return $"{ parsedContainerWidth.Value * parsedWidth.Value / 100}px";

            return parsedWidth.ToString();
        }

        public string GetColumnClass()
        {
            var parsedWidth = GetParsedWidth();
            var formattedClassNb = parsedWidth.Value.ToString().Replace('.', '-');

            // LR: Default to pixels
            string className = $"mj-column-px-{formattedClassNb}";

            // LR: Override for percetages
            if (parsedWidth.Unit.Equals("%", StringComparison.InvariantCultureIgnoreCase))
                className = $"mj-column-per-{formattedClassNb}";

            HtmlSkeleton.AddMediaQuery(className, parsedWidth);

            return className;
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "background-color", string.Empty },
                { "border", string.Empty },
                { "border-bottom", string.Empty },
                { "border-left", string.Empty },
                { "border-radius", string.Empty },
                { "border-right", string.Empty },
                { "border-top", string.Empty },
                { "direction", "ltr" },
                { "inner-background-color", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "padding-top", string.Empty },
                { "inner-border", string.Empty },
                { "inner-border-bottom", string.Empty },
                { "inner-border-left", string.Empty },
                { "inner-border-radius", string.Empty },
                { "inner-border-right", string.Empty },
                { "inner-border-top", string.Empty },
                { "padding", string.Empty },
                { "vertical-align", "top" },
                { "width", string.Empty },
            };
        }

        public override void SetupStyles()
        {
            bool bHasGutter = HasGutter();

            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "font-size", "0px" },
                { "text-align", "left" },
                { "direction", GetAttribute("direction") },
                { "display", "inline-block" },
                { "vertical-align", GetAttribute("vertical-align") },
                { "width", GetMobileWidth() },
            });

            if (bHasGutter)
            {
                StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                    { "background-color", GetAttribute("inner-background-color") },
                    { "border", GetAttribute("border") },
                    { "border-bottom", GetAttribute("border-bottom") },
                    { "border-left", GetAttribute("border-left") },
                    { "border-radius", GetAttribute("border-radius") },
                    { "border-right", GetAttribute("border-right") },
                    { "border-top", GetAttribute("border-top") },
                    { "vertical-align", GetAttribute("vertical-align") }
                });
            }
            else
            {
                StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                    { "background-color", GetAttribute("background-color") },
                    { "border", GetAttribute("border") },
                    { "border-bottom", GetAttribute("border-bottom") },
                    { "border-left", GetAttribute("border-left") },
                    { "border-radius", GetAttribute("border-radius") },
                    { "border-right", GetAttribute("border-right") },
                    { "border-top", GetAttribute("border-top") },
                    { "vertical-align", GetAttribute("vertical-align") }
                });
            }

            StyleLibraries.AddStyleLibrary("tdOutlook", new Dictionary<string, string>() {
                { "vertical-align", GetAttribute("vertical-align") },
                { "width", GetWidthAsPixel() }
            });

            StyleLibraries.AddStyleLibrary("gutter", new Dictionary<string, string>() {
                { "background-color", GetAttribute("background-color") },
                { "border", GetAttribute("border") },
                { "border-bottom", GetAttribute("border-bottom") },
                { "border-left", GetAttribute("border-left") },
                { "border-radius", GetAttribute("border-radius") },
                { "border-right", GetAttribute("border-right") },
                { "border-top", GetAttribute("border-top") },
                { "vertical-align", GetAttribute("vertical-align") },
                { "padding", GetAttribute("padding") },
                { "padding-top", GetAttribute("padding-top") },
                { "padding-right", GetAttribute("padding-right") },
                { "padding-bottom", GetAttribute("padding-bottom") },
                { "padding-left", GetAttribute("padding-left") }
            });
        }

        // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-column/src/index.js#L250
        /// <summary>
        /// RenderWrappedChildren
        /// </summary>
        /// <returns></returns>
        public override string RenderChildren()
        {
            if (!this.Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var childComponent in Children)
            {
                string childContent = childComponent.RenderMjml();

                if (string.IsNullOrWhiteSpace(childContent))
                    continue;

                sb.Append($@"
                    <tr>
                        <td {HtmlAttributes(new Dictionary<string, string>() {
                                { "align", GetAttribute("align") },
                                { "vertical-align", GetAttribute("vertical-align") },
                                { "class", GetAttribute("css-class") },
                                { "style", InlineCss(new Dictionary<string, string> {
                                        { "background", GetAttribute("container-background-color") },
                                        { "font-size", "0px" },
                                        { "padding", GetAttribute("padding") },
                                        { "padding-top", GetAttribute("padding-top") },
                                        { "padding-right", GetAttribute("padding-right") },
                                        { "padding-bottom", GetAttribute("padding-bottom") },
                                        { "padding-left", GetAttribute("padding-left") },
                                        { "word-break", GetAttribute("break-word") }
                                    })
                                },
                            })}
                        >
                ");

                sb.Append(childContent);

                sb.Append($@"
                        </td>
                    </tr>
                ");
            }

            return sb.ToString();
        }

        public string RenderColumn()
        {
            return $@"
                <table {HtmlAttributes(new Dictionary<string, string> {
                           { "border", "0" },
                           { "cellpadding", "0" },
                           { "cellspacing", "0" },
                           { "role", "presentation" },
                           { "width", "100%" },
                       })}
                >
                { this.RenderChildren() }
                </table>
            ";
        }

        public string RenderGutter()
        {
            return $@"
                <table {HtmlAttributes(new Dictionary<string, string> {
                           { "border", "0" },
                           { "cellpadding", "0" },
                           { "cellspacing", "0" },
                           { "role", "presentation" },
                           { "width", "100%" },
                       })}
                >
                    <tbody>
                        <tr>
                            <td {HtmlAttributes(new Dictionary<string, string> {
                                    { "style", "gutter" }
                                })}
                            >
                                {this.RenderColumn()}
                            </td>
                        </tr>
                    </tbody>
                </table>
            ";
        }

        public override string RenderMjml()
        {
            string classesName = $"{GetColumnClass()} mj-outlook-group-fix";

            if (HasAttribute("css-class"))
                classesName += $" {GetAttribute("css-class")}";

            bool bHasGutter = HasGutter();

            return $@"
                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "class", classesName },
                        { "style", "div" }
                     })}
                >
                {(bHasGutter ? this.RenderGutter() : this.RenderColumn())}
                </div>
            ";
        }
    }
}