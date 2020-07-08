using Mjml.Core.Component;
using Mjml.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-section/src/index.js
    public class MjmlSectionComponent : BodyComponent
    {
        public MjmlSectionComponent(XElement element) : base(element)
        {
        }

        public bool HasBackground()
        {
            return HasAttribute("full-width");
        }

        public bool IsFullWidth()
        {
            return HasAttribute("full-width") ?
                GetAttribute("full-width").Equals("full-width", StringComparison.InvariantCultureIgnoreCase) :
                false;
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string> {
                { "background-color", null },
                { "background-repeat", "repeat" },
                { "background-size", "auto" },
                { "background-url", null },
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

                { "background-position", "top center" },
                { "background-position-x", "none" },
                { "background-position-y", "none" },
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
                { "max-width", HtmlSkeleton.ContainerWidth},// TODO: IMPLEMENT
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
                        {"class", CssHelper.SuffixCssClasses(GetAttribute("css-class"), "outlook") },
                        {"style",
                            InlineCss( new Dictionary<string, string> {
                                { "width", HtmlSkeleton.ContainerWidth }
                            })
                        },
                        {"width", CssUnitParser.Parse(HtmlSkeleton.ContainerWidth).Value.ToString() } // -> width="600"
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
                if (!(childComponent is MjmlColumnComponent))
                    continue;

                string childContent = childComponent.RenderMjml();

                if (string.IsNullOrWhiteSpace(childContent))
                    continue;

                sb.Append($@"
                    <!--[if mso | IE]>
                        <td {HtmlAttributes(new Dictionary<string, string>
                            {
                                {"align", GetAttribute("align") },
                                {"class", CssHelper.SuffixCssClasses(GetAttribute("css-class"), "outlook") },
                                {"style", (childComponent as MjmlColumnComponent).Styles("tdOutlook") }
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
            bool bHasBackground = HasBackground();
            bool bIsFullWidth = IsFullWidth();

            return $@"
                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "class", bIsFullWidth ? null : GetAttribute("css-class") },
                        { "style", "div" }
                     })}>

                    {(bHasBackground ?
                        $@"<div {HtmlAttributes(new Dictionary<string, string> {
                            { "style", "innerDiv" }
                        })}" : null)}

                    <table {HtmlAttributes(new Dictionary<string, string> {
                            { "align", "center"},
                            { "background", bIsFullWidth ? null : GetAttribute("background-url")},
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
                    {(bHasBackground ? "</div>" : null)}
                </div>
            ";
        }

        public string RenderFullWidth()
        {
            bool bHasBackground = HasBackground();

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
            bool bHasBackground = HasBackground();

            string section = this.RenderSection();

            return $@"
                {this.RenderBefore()}
                {(bHasBackground ? string.Empty : section) /* TODO: this.renderWithBackground(section) */}
                {this.RenderAfter()}
            ";
        }

        public override string RenderMjml()
        {
            return IsFullWidth() ? this.RenderFullWidth() : this.RenderSimple();
        }
    }
}