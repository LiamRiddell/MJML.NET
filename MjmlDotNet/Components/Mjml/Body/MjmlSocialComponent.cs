using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    internal class MjmlSocialComponent : BodyComponent
    {
        public MjmlSocialComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlSocial;
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("tableVertical", new Dictionary<string, string>() {
                { "margin", "0px" },
            });
        }

        public string[] GetSocialElementAttributes()
        {
            return new string[]
            {
                "border-radius",
                "color",
                "font-family",
                "font-size",
                "font-weight",
                "font-style",
                "icon-size",
                "icon-height",
                "icon-padding",
                "text-padding",
                "line-height",
                "text-decoration"
            };
        }

        public override string RenderChildren()
        {
            if (!Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            // LR: Pass attributes from parent social component to social element components
            PassAttributesToChildren(GetSocialElementAttributes());

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
                    var socialElementComponent = childComponent as MjmlSocialElementComponent;

                    sb.Append($@"
                        <!--[if mso | IE]>
                            <td>
                        <![endif]-->
                                <table {socialElementComponent.HtmlAttributes(new Dictionary<string, string>() {
                                        { "align", socialElementComponent.GetAttribute("align") },
                                        { "border", "0" },
                                        { "cellpadding", "0" },
                                        { "cellspacing", "0" },
                                        { "role", "presentation" },
                                        { "style", "float:none;display:inline-table;" }
                                    })}
                                >
                                    {childContent}
                                </table>
                        <!--[if mso | IE]>
                            </td>
                        <![endif]-->
                    ");
                }
            }

            return sb.ToString();
        }

        public string RenderHorizontal()
        {
            return $@"
                <!--[if mso | IE]>
                 <table
                  {HtmlAttributes(new Dictionary<string, string> {
                    { "border", "0" },
                    { "cellpadding", "0" },
                    { "cellspacing", "0" },
                    { "role", "presentation" },
                    { "style", "tableVertical" },
                  })}
                >
                    <tr>
                <![endif]-->

                {RenderChildren()}

                <!--[if mso | IE]>
                    </tr>
                </table>
              <![endif]-->
            ";
        }

        public string RenderVertical()
        {
            return $@"
                <table
                  {HtmlAttributes(new Dictionary<string, string> {
                    { "border", "0" },
                    { "cellpadding", "0" },
                    { "cellspacing", "0" },
                    { "role", "presentation" },
                    { "style", "tableVertical" },
                  })}
                >
                  {RenderChildren()}
                </table>
            ";
        }

        public override string RenderMjml()
        {
            return GetAttribute("mode").Equals("horizontal") ? RenderHorizontal() : RenderVertical();
        }
    }
}