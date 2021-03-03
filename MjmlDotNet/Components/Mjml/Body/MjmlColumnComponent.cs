using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Css;
using MjmlDotNet.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-column/src/index.js
    internal class MjmlColumnComponent : BodyComponent
    {
        public string ContainerWidth { get; set; } = null;
        public int ParentSectionColumnCount { get; set; }

        public MjmlColumnComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public bool HasGutter()
        {
            return HasAttribute("padding") ||
                   HasAttribute("padding-bottom") ||
                   HasAttribute("padding-left") ||
                   HasAttribute("padding-right") ||
                   HasAttribute("padding-top");
        }

        public int GetSectionColumnCount()
        {
            return Element.ParentElement.ChildNodes.Count(e => e.NodeType.Equals(NodeType.Element));
        }

        public override CssBoxModel GetBoxModel()
        {
            // LR: Default to the outmost container
            CssParsedUnit containerWidth = CssUnitParser.Parse(HtmlSkeleton.ContainerWidth);

            // LR: Get Padding
            var paddings =
                GetShorthandAttributeValue("padding", "right") +
                GetShorthandAttributeValue("padding", "left");

            // LR: Get Borders
            var borders =
                GetShorthandBorderValue("right") +
                GetShorthandBorderValue("left");

            // LR: Get inner-borders
            float innerBorders =
               GetShorthandAttributeValue("inner-border", "left") +
               GetShorthandAttributeValue("inner-border", "right");

            // LR: All padding
            float allPaddings = paddings + borders + innerBorders;

            // LR: Try and get the parents calculated Box Model size.
            if (HasParentComponent())
            {
                var parent = GetParentComponent() as BodyComponent;

                // LR: Get columns in this section
                ParentSectionColumnCount = GetSectionColumnCount();

                // LR: Parent section width
                var sectionWidth = parent.CssBoxModel.BoxWidth;

                // LR: Column has width attribute?
                if (HasAttribute("width"))
                    ContainerWidth = GetAttribute("width");
                else
                    ContainerWidth = $"{sectionWidth / ParentSectionColumnCount}px";

                // LR: Parse the calculated width
                CssParsedUnit parsedWidth = CssUnitParser.Parse(ContainerWidth);

                // LR: Handle Percentage values
                if (parsedWidth.Unit.Equals("%", StringComparison.InvariantCultureIgnoreCase))
                {
                    parsedWidth.Value = (sectionWidth * parsedWidth.Value / 100);
                    ContainerWidth = $"{parsedWidth.Value - allPaddings}";
                }
                else
                {
                    ContainerWidth = $"{parsedWidth.Value - allPaddings}px";
                }

                // LR: Calculated column width
                var columnWidth = CssUnitParser.Parse(ContainerWidth);

                return new CssBoxModel(
                    parsedWidth.Value,
                    borders,
                    paddings,
                    columnWidth.Value
                );
            }

            return new CssBoxModel(
                containerWidth.Value,
                borders,
                paddings,
                containerWidth.Value);
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
            var parsedContainerWidth = CssUnitParser.Parse(ContainerWidth);

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
            return GlobalDefaultAttributes.Body.MjmlColumn;
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
                    { "border", GetAttribute("inner-border") },
                    { "border-bottom", GetAttribute("inner-border-bottom") },
                    { "border-left", GetAttribute("inner-border-left") },
                    { "border-radius", GetAttribute("inner-border-radius") },
                    { "border-right", GetAttribute("inner-border-right") },
                    { "border-top", GetAttribute("inner-border-top") }
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

                if (childComponent.IsRawElement())
                {
                    sb.Append(childContent);
                }
                else
                {
                    var component = childComponent as BodyComponent;

                    sb.Append($@"
                        <tr>
                            <td {component.HtmlAttributes(new Dictionary<string, string>() {
                                    { "align", component.GetAttribute("align") },
                                    { "vertical-align", component.GetAttribute("vertical-align") },
                                    { "class", component.GetAttribute("css-class") },
                                    { "style", component.InlineCss(new Dictionary<string, string> {
                                            { "background", component.GetAttribute("container-background-color") },
                                            { "vertical-align", component.GetAttribute("vertical-align") },
                                            { "font-size", "0px" },
                                            { "padding", component.GetAttribute("padding") },
                                            { "padding-top", component.GetAttribute("padding-top") },
                                            { "padding-right", component.GetAttribute("padding-right") },
                                            { "padding-bottom", component.GetAttribute("padding-bottom") },
                                            { "padding-left", component.GetAttribute("padding-left") },
                                            { "word-break", "break-word" }
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
                           { "style", "table" },
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