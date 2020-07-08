using Mjml.Core.Component;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.HtmlComponents
{
    /// <summary>
    /// HtmlText outputs the text content of html node.
    /// </summary>
    public class HtmlRawComponent : BodyComponent
    {
        public HtmlRawComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
            };
        }

        public override void SetAttributes()
        {
            var attributes = Element.Attributes();

            foreach (var attribute in attributes)
            {
                string userAttributeName = attribute.Name.LocalName.ToLowerInvariant();
                string userAttributeValue = attribute.Value;

                // Passth all attributes to the element for output
                Attributes.Add(userAttributeName, userAttributeValue);
            }
        }

        public override string RenderMjml()
        {
            return $@"
            <{Element.Name.LocalName} {HtmlAttributes(Attributes)}>
                {this.RenderChildren()}
            </{Element.Name.LocalName}>";
        }
    }
}