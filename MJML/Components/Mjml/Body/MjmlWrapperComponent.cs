using AngleSharp.Dom;
using MjmlDotNet.Core.Component;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-wrapper/src/index.js
    public class MjmlWrapperComponent : MjmlSectionComponent
    {
        public MjmlWrapperComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
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

                    sb.Append($@"
                        <!--[if mso | IE]>
                            <tr>
                                <td {component.HtmlAttributes(new Dictionary<string, string>() {
                                        { "align", component.GetAttribute("align") },
                                        { "class", CssHelper.SuffixCssClasses(component.GetAttribute("css-class"), "outlook") },
                                        { "width", $"{GetContainerOuterWidth()}px" },
                                    })}
                                >
                        <![endif]-->
                    ");

                    sb.Append(childContent);

                    sb.Append($@"
                        <!--[if mso | IE]>
                                </td>
                            </tr>
                        <![endif]-->
                    ");
                }
            }

            return sb.ToString();
        }
    }
}