using Mjml.Core;
using Mjml.Core.Component;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    public class MjmlRawComponent : BodyComponent
    {
        public MjmlRawComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
            };
        }

        public override string RenderMjml()
        {
            return $@"
            <{Element.Name.LocalName}>
                {this.RenderChildren()}
            </{Element.Name.LocalName}>";
        }
    }
}