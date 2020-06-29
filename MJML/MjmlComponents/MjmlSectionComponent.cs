using Mjml.Core;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents
{
    public class MjmlSectionComponent : BodyComponent
    {
        public MjmlSectionComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string> {
                { "background-color", "red" }
            };
        }

        public override string RenderMjml()
        {
            // TODO: HtmlHelper.IfMsoIe(""</td>"")
            return $@"
            {TagHelpers.ConditionalTag("<tr>")}
                {TagHelpers.ConditionalTag("<td htmlAttributesFunc=\"align, class, style\">")}
                    {this.RenderChildren()}
                {TagHelpers.ConditionalTag("</td>")}
            {TagHelpers.ConditionalTag("</tr>")}";
        }
    }
}