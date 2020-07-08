using Mjml.Core;
using Mjml.Core.Component;
using Mjml.Extensions;
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
            return HasAttribute("background-url");
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
                { "background-position-x", null },
                { "background-position-y", null },
            };
        }

        public string GetBackgroundString()
        {
            var backgroundPosition = GetBackgroundPosition();
            return $"{backgroundPosition.X} {backgroundPosition.Y}";
        }

        public string GetBackground()
        {
            if (!HasBackground())
            {
                return $"{GetAttribute("background-color")}";
            }

            return $"{GetAttribute("background-color")} url({GetAttribute("background-url")}) {GetBackgroundString()} / {GetAttribute("background-size")} {GetAttribute("background-repeat")}";
        }

        public override void SetupStyles()
        {
            bool isFullWidth = IsFullWidth();

            Dictionary<string, string> background = HasBackground() ? new Dictionary<string, string>
            {
                { "background", GetBackground() },
                // background size, repeat and position has to be seperate since yahoo does not support shorthand background css property
                { "background-position", GetBackgroundString() },
                { "background-repeat", GetAttribute("background-repeat") },
                { "background-size", GetAttribute("background-size") },
            } :
            new Dictionary<string, string>
            {
                { "background", GetAttribute("background-color") },
                { "background-color", GetAttribute("background-color") },
            };

            StyleLibraries.AddStyleLibrary("tableFullwidth",
                isFullWidth ? background.MergeLeft(
                    new Dictionary<string, string> {
                        { "width", "100%" },
                        { "border-radius", GetAttribute("border-radius") },
                    }) : 
                    new Dictionary<string, string>
                    {
                        { "width", "100%" },
                        { "border-radius", GetAttribute("border-radius") },
                    });

            StyleLibraries.AddStyleLibrary("table",
                !isFullWidth ? background.MergeLeft(
                    new Dictionary<string, string> {
                        { "width", "100%" },
                        { "border-radius", GetAttribute("border-radius") },
                    }) :
                    new Dictionary<string, string>
                    {
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

            StyleLibraries.AddStyleLibrary("div",
                isFullWidth ? background.MergeLeft(
                    new Dictionary<string, string> {
                        { "margin", "0px auto" },
                        { "border-radius", GetAttribute("border-radius") },
                        { "max-width", HtmlSkeleton.ContainerWidth},
                    }) :
                    new Dictionary<string, string>
                    {
                        { "margin", "0px auto" },
                        { "border-radius", GetAttribute("border-radius") },
                        { "max-width", HtmlSkeleton.ContainerWidth},
                    });

            StyleLibraries.AddStyleLibrary("innerDiv", new Dictionary<string, string>() {
                { "line-height", "0" },
                { "font-size", "0" }
            });
        }

        public CssCoordinate ParseBackgroundPosition()
        {
            var posSplit = GetAttribute("background-position").Split(' ');

            // LR: One Position
            if (posSplit.Length.Equals(1))
            {
                var value = posSplit[0];

                if (value.Equals("top", StringComparison.InvariantCultureIgnoreCase) ||
                    value.Equals("bottom", StringComparison.InvariantCultureIgnoreCase))
                {
                    return new CssCoordinate("center", value);
                }

                return new CssCoordinate(value, "center");
            }

            // LR: Two Positions
            if (posSplit.Length.Equals(2))
            {
                var value1 = posSplit[0];
                var value2 = posSplit[1];

                if (value1.Equals("top", StringComparison.InvariantCultureIgnoreCase) ||
                    value1.Equals("bottom", StringComparison.InvariantCultureIgnoreCase) ||

                    value1.Equals("center", StringComparison.InvariantCultureIgnoreCase) &&
                    value2.Equals("left", StringComparison.InvariantCultureIgnoreCase) ||
                    value2.Equals("right", StringComparison.InvariantCultureIgnoreCase))
                {
                    return new CssCoordinate(value2, value1);
                }

                return new CssCoordinate(value1, value2);
            }

            // LR: 2 + positions not support use default position instead
            return new CssCoordinate("center", "top");
        }

        public CssCoordinate GetBackgroundPosition()
        {
            var position = ParseBackgroundPosition();

            return new CssCoordinate(
                HasAttribute("background-position-x") ? GetAttribute("background-position-x") : position.X,
                HasAttribute("background-position-y") ? GetAttribute("background-position-y") : position.Y
            );
        }

        public CssCoordinate CalculateBackgroundAxisOrigin(string axis, CssCoordinate coordinate)
        {
            bool isX = axis.Equals("x", StringComparison.InvariantCultureIgnoreCase);
            bool isBackgroundRepeat = HasAttribute("background-repeat") ? GetAttribute("background-repeat").Equals("repeat", StringComparison.InvariantCultureIgnoreCase) : false;

            string position = isX ? coordinate.X : coordinate.Y;
            string origin = isX ? coordinate.X : coordinate.Y;

            float positionFloat = 0;
            float originFloat = 0;

            if (position.Contains("%"))
            {
                var percentage = CssUnitParser.Parse(position);

                if (isBackgroundRepeat)
                {
                    positionFloat = percentage.Value;
                    originFloat = percentage.Value;
                }
                else
                {
                    float computed = (-50 + percentage.Value * 100) / 100;
                    positionFloat = computed;
                    originFloat = computed;
                }
            }
            else if (isBackgroundRepeat)
            {
                positionFloat = isX ? 0.5f : 0f;
                originFloat = isX ? 0.5f : 0f;
            }
            else
            {
                positionFloat = isX ? 0f : -0.5f;
                originFloat = isX ? 0f : -0.5f;
            }

            return new CssCoordinate(originFloat.ToString(), positionFloat.ToString());
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
                string childContent = childComponent.RenderMjml();

                if (string.IsNullOrWhiteSpace(childContent))
                    continue;

                if (childComponent.IsRawElement())
                {
                    sb.Append(childContent);
                }
                else
                {
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
                        })}>" : null)}

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

        public string RenderWithBackground(string content)
        {
            CssParsedUnit containerWidth = CssUnitParser.Parse(HtmlSkeleton.ContainerWidth);

            bool isFullWidth = IsFullWidth();
            bool isPercentage = containerWidth.Unit.Equals("%", StringComparison.InvariantCultureIgnoreCase);

            CssCoordinate backgroundPosition = GetBackgroundPosition();

            // LR: Convert direction to Percentage X
            switch (backgroundPosition.X)
            {
                case "left":
                    backgroundPosition.X = "0%";
                    break;

                case "center":
                    backgroundPosition.X = "50%";
                    break;

                case "right":
                    backgroundPosition.X = "100%";
                    break;

                default:
                    if (!backgroundPosition.X.ToString().Contains("%"))
                        backgroundPosition.X = "50%";
                    break;
            }

            // LR: Convert direction to Percentage Y
            switch (backgroundPosition.Y)
            {
                case "top":
                    backgroundPosition.Y = "0%";
                    break;

                case "center":
                    backgroundPosition.Y = "50%";
                    break;

                case "bottom":
                    backgroundPosition.Y = "100%";
                    break;

                default:
                    if (!backgroundPosition.Y.ToString().Contains("%"))
                        backgroundPosition.Y = "0%";
                    break;
            }

            // LR: Calculate position to origin
            // X = Axis Origin, Y = Axis Position
            // This logic is different when using repeat or no-repeat
            var originX = CalculateBackgroundAxisOrigin("x", backgroundPosition);
            var originY = CalculateBackgroundAxisOrigin("y", backgroundPosition);

            Dictionary<string, string> vSizeAttributes = new Dictionary<string, string>();

            // If background size is either cover or contain, we tell VML to keep the aspect
            // and fill the entire element.
            if (GetAttribute("background-size").Equals("cover", StringComparison.InvariantCultureIgnoreCase) ||
                GetAttribute("background-size").Equals("contain", StringComparison.InvariantCultureIgnoreCase))
            {
                vSizeAttributes = new Dictionary<string, string>
                {
                    { "size", "1,1" },
                    {
                        "aspect",
                        GetAttribute("background-size").Equals("cover", StringComparison.InvariantCultureIgnoreCase) ? "atleast" : "atmost"
                    },
                };
            }
            else if (!GetAttribute("background-size").Equals("auto", StringComparison.InvariantCultureIgnoreCase))
            {
                string backgroundSize = GetAttribute("background-size");
                var bgSplit = backgroundSize.Split(' ');

                if (bgSplit.Length.Equals(1))
                {
                    vSizeAttributes = new Dictionary<string, string>
                    {
                        { "size", GetAttribute("background-size") },
                        { "aspect", "atmost" }, // reproduces height auto
                    };
                }
                else
                {
                    vSizeAttributes = new Dictionary<string, string>
                    {
                        { "size", backgroundSize.Replace(' ', ',') },
                    };
                }
            }

            var vmlType = GetAttribute("background-repeat").Equals("no-repeat", StringComparison.InvariantCultureIgnoreCase) ? "frame" : "tile";

            if (GetAttribute("background-size").Equals("auto", StringComparison.InvariantCultureIgnoreCase))
            {
                vmlType = "tile"; // If no size provided, keep old behavior because outlook can't use original image size with "frame"

                // Also ensure that images are still cropped the same way
                originX = new CssCoordinate("0.5", "0.5");
                originY = new CssCoordinate("0", "0");
            }

            return $@"
                <!--[if mso | IE]>
                <v:rect {HtmlAttributes(new Dictionary<string, string> {
                    { "style",
                        isFullWidth ?
                        InlineCss(new Dictionary<string, string>{ { "mso-width-percent", "1000"} }) :
                        InlineCss(new Dictionary<string, string>{ { "width", HtmlSkeleton.ContainerWidth} })
                    },
                    { "xmlns:v", "urn:schemas-microsoft-com:vml"},
                    { "fill", "true"},
                    { "stroke", "false"},
                })}>
                    <v:fill {HtmlAttributes(new Dictionary<string, string>
                            {
                                { "origin", $"{originX.X}, {originY.X}"},
                                { "position", $"{originX.Y}, {originY.Y}" },
                                { "src", GetAttribute("background-url") },
                                { "color", GetAttribute("background-color") },
                                { "type", vmlType }
                            }.MergeLeft(vSizeAttributes))} />
                    <v:textbox style=""mso-fit-shape-to-text:true"" inset=""0,0,0,0"">
                <![endif]-->
                {content}
                <!--[if mso | IE]>
                    </v:textbox>
                </v:rect>
                <![endif]-->
            ";
        }

        public string RenderFullWidth()
        {
            bool bHasBackground = HasBackground();

            string content = bHasBackground ?
                RenderWithBackground($@"
                    {this.RenderBefore()}
                    {this.RenderSection()}
                    {this.RenderAfter()}
                ") :
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
                {(bHasBackground ? RenderWithBackground(section) : section)}
                {this.RenderAfter()}
            ";
        }

        public override string RenderMjml()
        {
            return IsFullWidth() ? this.RenderFullWidth() : this.RenderSimple();
        }
    }
}