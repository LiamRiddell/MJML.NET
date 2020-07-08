using Mjml.Core.Component;
using Mjml.Core.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents
{
    public class MjmlRootComponent : BodyComponent
    {
        public MjmlRootComponent(XElement element, BaseComponent parent) : base(element, parent)
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
            return this.RenderChildren();
        }
    }
}