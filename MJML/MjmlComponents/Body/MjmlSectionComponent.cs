using Mjml.Core;
using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    public class MjmlSectionComponent : BodyComponent
    {
        public MjmlSectionComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string> {
                { "background-color", string.Empty },
                { "background-position", "top center" },
                { "background-position-x", "none" },
                { "background-position-y", "none" },
                { "background-repeat", "repeat" },
                { "background-size", "auto" },
                { "background-url", string.Empty },
                { "border", string.Empty },
                { "border-bottom", string.Empty },
                { "border-left", string.Empty },
                { "border-radius", string.Empty },
                { "border-right", string.Empty },
                { "border-top", string.Empty },
                { "css-class", string.Empty },
                { "direction", "ltr" },
                { "full-width", string.Empty },
                { "padding", "20px 0" },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "padding-top", string.Empty },
                { "text-align", "center" },
                { "text-padding", "4px 4px 4px 0" },
            };
        }

        public override void SetupStyles()
        {
            // TODO: Add background support
            // https://github.com/mjmlio/mjml/blob/master/packages/mjml-section/src/index.js#L56

            StyleLibraries.AddStyleLibrary("tableFullwidth", new Dictionary<string, string>() {
                { "width", "100%" },
                { "border-radius", GetAttribute("border-radius") },
            });

            StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                { "width", "100%" },
                { "border-radius", GetAttribute("border-radius") },
            });

            StyleLibraries.AddStyleLibrary("td", new Dictionary<string, string>() {
                { "border", GetAttribute("border") },
                { "border-bottom", GetAttribute("border-bottom") },
                { "border-left", GetAttribute("border-left") },
                { "border-right", GetAttribute("border-right") },
                { "border-top", GetAttribute("border-top") },
                { "direction", GetAttribute("direction") },
                { "font-size", "0px"},
                { "padding", GetAttribute("padding") },
                { "padding-bottom", GetAttribute("padding-bottom") },
                { "padding-left", GetAttribute("padding-left") },
                { "padding-right", GetAttribute("padding-right") },
                { "padding-top", GetAttribute("padding-top") },
                { "text-align", GetAttribute("text-align") },
            });

            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "margin", "0px auto" },
                { "border-radius", GetAttribute("border-radius") },
                { "max-width", "600px"},// TODO: IMPLEMENT
            });

            StyleLibraries.AddStyleLibrary("innerDiv", new Dictionary<string, string>() {
                { "line-height", "0" },
                { "font-size", "0" }
            });
        }

        public string RenderBefore()
        {
            return $@"
                <!--[if mso | IE]>
                <table
                    {HtmlAttributes(new Dictionary<string, string> {
                        {"align", "center" },
                        {"border", "0" },
                        {"cellpadding", "0" },
                        {"cellspacing", "0" },
                        {"class", "" }, // TODO: suffixCssClasses(this.getAttribute('css-class'), 'outlook'),
                        {"style", "0" }, // TODO: { width: `${containerWidth}` },
                        {"width", "100px" } // TODO: parseInt(containerWidth, 10),
                    })}
                >
                    <tr>
                        <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
                <![endif]-->
            ";
        }

        public string RenderAfter()
        {
            return $@"
               <!--[if mso | IE]>
                  </td>
                </tr>
              </table>
              <![endif]-->
            ";
        }

        // https://github.com/mjmlio/mjml/blob/master/packages/mjml-section/src/index.js#L221
        /// <summary>
        /// RenderWrappedChildren
        /// </summary>
        /// <returns></returns>
        public override string RenderChildren()
        {
            if (!this.Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append(@"
                <!--[if mso | IE]>
                    <tr>
                <![endif]-->");

            foreach (var childComponent in Children)
            {
                string childContent = childComponent.RenderMjml();

                if (string.IsNullOrWhiteSpace(childContent))
                    continue;

                sb.Append($@"
                    <!--[if mso | IE]>
                        <td {HtmlAttributes(new Dictionary<string, string>
                            {
                                {"align", GetAttribute("align") },
                                {"class", GetAttribute("css-class") }, // TODO: suffixCssClasses(component.getAttribute('css-class'),'outlook'),
                                {"style", "tdOutlook" }
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

            sb.Append($@"
                <!--[if mso | IE]>
                    </tr>
                <![endif]-->
            ");

            return sb.ToString();
        }

        public string RenderSection()
        {
            bool bHasBackground = false; // TODO: this.hasBackground()

            return $@"
                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "class", GetAttribute("css-class") }, // this.isFullWidth() ? null : this.getAttribute('css-class'),
                        { "style", "div" }
                     })}>

                    {(bHasBackground ?
                        $@"<div {HtmlAttributes(new Dictionary<string, string> {
                            { "style", "innerDiv" }
                        })}" : "")}

                    <table {HtmlAttributes(new Dictionary<string, string> {
                            { "align", "center"},
                            { "background", string.Empty}, // TODO: background: this.isFullWidth() ? null : this.getAttribute('background-url'),
                            { "border", "0"},
                            { "cellpadding", "0"},
                            { "cellspacing", "0"},
                            { "role", "presentation"},
                            { "style", "table" }
                        })}
                    >
                        <tbody>
                            <tr>
                                <td {HtmlAttributes(new Dictionary<string, string> {
                                        {"style", "td"}
                                    })}
                                >
                                    <!--[if mso | IE]>
                                        <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                    <![endif]-->
                                    { this.RenderChildren() /* LR - RenderChildrenWrapped */ }
                                    <!--[if mso | IE]>
                                      </table>
                                    <![endif]-->
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    {(bHasBackground ? "</div>" : string.Empty)}
                </div>
            ";
        }

        public string RenderFullWidth()
        {
            bool bHasBackground = false; // TODO: this.hasBackground()

            string content = bHasBackground ?
                $@"" : // TODO: https://github.com/mjmlio/mjml/blob/master/packages/mjml-section/src/index.js#L452
                $@"
                    {this.RenderBefore()}
                    {this.RenderSection()}
                    {this.RenderAfter()}
                ";

            return $@"
                <table {HtmlAttributes(new Dictionary<string, string> {
                            {"align", "center" },
                            {"class", GetAttribute("css-class") },
                            {"background", GetAttribute("background-url") },
                            {"border", "0" },
                            {"cellpadding", "0" },
                            {"cellspacing", "0" },
                            {"role", "presentation" },
                            {"style", "tableFullwidth" }
                        })}
                >
                    <tbody>
                        <tr>
                            <td>
                                {content}
                            </td>
                        </tr>
                    </tbody>
                </table>
            ";
        }

        public string RenderSimple()
        {
            bool bHasBackground = false; // TODO: this.hasBackground()
            string section = this.RenderSection();

            return $@"
                {this.RenderBefore()}
                {(bHasBackground ? string.Empty : section) /* TODO: this.renderWithBackground(section) */}
                {this.RenderAfter()}
            ";
        }

        public override string RenderMjml()
        {
            bool bIsFullwidth = false; // TODO: this.isFullWidth();

            return bIsFullwidth ? this.RenderFullWidth() : this.RenderSimple();
        }
    }
}