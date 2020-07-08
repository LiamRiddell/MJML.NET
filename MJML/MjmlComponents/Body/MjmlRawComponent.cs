using Mjml.Core.Component;
using Mjml.HtmlComponents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-raw/src/index.js
    public class MjmlRawComponent : BodyComponent
    {
        public MjmlRawComponent(XElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
            };
        }

        public override string RenderChildren()
        {
            if (!this.Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            using (var reader = Element.CreateReader())
            {
                reader.MoveToContent();
                sb.Append(reader.ReadInnerXml());
            }

            return sb.ToString();
        }

        public override string RenderMjml()
        {
            return this.RenderChildren();
        }
    }
}