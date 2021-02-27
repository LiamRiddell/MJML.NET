using AngleSharp.Dom;
using MjmlDotNet.Core.Component;
using MjmlDotNet.Core.Css;
using MjmlDotNet.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-group/src/index.js
    public class MjmlGroupComponent : BodyComponent
    {
        public string ContainerWidth { get; set; } = null;
        public int ParentSectionColumnCount { get; set; }

        public MjmlGroupComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "background-colour", null },
                { "direction", "ltr" },
                { "vertical-align", null },
                { "width", null }
            };
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "font-size", "0" },
                { "line-height", "0" },
                { "text-align", "left" },
                { "display", "inline-block" },
                { "width", "100%" },
                { "direction", GetAttribute("direction") },
                { "vertical-align", GetAttribute("vertical-align") },
                { "background-colour", GetAttribute("background-colour") }
            });

            StyleLibraries.AddStyleLibrary("tdOutlook", new Dictionary<string, string>() {
                { "vertical-align", GetAttribute("vertical-align") },
                { "width", $"{GetWidthAsPixel()}" }
            });
        }

        public override CssBoxModel GetBoxModel()
        {
            // LR: Default to the outmost container
            CssParsedUnit containerWidth = CssUnitParser.Parse(HtmlSkeleton.ContainerWidth);

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
                    ContainerWidth = $"{parsedWidth.Value}px";
                }
                else
                {
                    ContainerWidth = $"{parsedWidth.Value}px";
                }

                // LR: Calculated column width
                var columnWidth = CssUnitParser.Parse(ContainerWidth);

                return new CssBoxModel(
                    parsedWidth.Value,
                    0,
                    0,
                    columnWidth.Value
                );
            }

            return new CssBoxModel(
                containerWidth.Value,
                0,
                0,
                containerWidth.Value);
        }

        public int GetSectionColumnCount()
        {
            return Element.Parent.ChildNodes.Count(e => e.NodeType.Equals(NodeType.Element));
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

        public string GetElementWidth(string width)
        {
            if (string.IsNullOrWhiteSpace(width))
            {
                var parsedContainerWidth = CssUnitParser.Parse(ContainerWidth);
                float columnWidth = parsedContainerWidth.Value / GetSectionColumnCount();
                return $"{columnWidth}px";
            }

            var parsedWidth = CssUnitParser.Parse(width);

            if (parsedWidth.Unit.Equals("%", StringComparison.InvariantCultureIgnoreCase))
                return $"{ 100 * parsedWidth.Value / GetContainerInnerWidth()}px";

            return parsedWidth.ToString();
        }

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

                    // LR: Set the column to mobileWidth
                    component.SetAttribute("mobileWidth", "mobileWidth");

                    sb.Append($@"
                        <!--[if mso | IE]>
                        <td {component.HtmlAttributes(new Dictionary<string, string>() {
                                { "style", component.InlineCss(new Dictionary<string, string> {
                                        { "align", component.GetAttribute("align") },
                                        { "vertical-align", component.GetAttribute("vertical-align") },
                                        { "width", GetElementWidth(component.HasAttribute("width") ? component.GetAttribute("width") : (component as MjmlColumnComponent).GetWidthAsPixel()) },
                                    })
                                },
                            })}
                        >
                        <![endif]-->
                    ");

                    sb.Append(childContent);

                    sb.Append($@"
                        <!--[if mso | IE]>
                        </td>
                        <![endif]-->
                    ");
                }
            }

            return sb.ToString();
        }

        public override string RenderMjml()
        {
            string classesName = $"{GetColumnClass()} mj-outlook-group-fix";

            if (HasAttribute("css-class"))
                classesName += $" {GetAttribute("css-class")}";

            return $@"
                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "class", classesName },
                        { "style", "div" },
                     })}
                >
                    <!--[if mso | IE]>
                    <table {HtmlAttributes(new Dictionary<string, string> {
                        { "bgcolor", HasAttribute("background-color") ? GetAttribute("background-color").Equals("none") ? GetAttribute("background-color") : null : null },
                        { "border", "0" },
                        { "cellpadding", "0" },
                        { "cellspacing", "0" },
                        { "role", "presentation" },
                        })}
                    >
                        <tr>
                    <![endif]-->
                            {RenderChildren()}
                    <!--[if mso | IE]>
                      </tr>
                      </table>
                    <![endif]-->
                </div>
            ";
        }
    }
}