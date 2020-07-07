using System;
using System.Xml.Linq;

namespace Mjml.Core.Component
{
    public class HeadComponent : BaseComponent
    {
        public HeadComponent(XElement element) : base(element)
        {
        }

        public virtual void Handler()
        {
            throw new Exception($"Inherited HeadComponent missing Handler()");
        }

        public override string RenderMjml()
        {
            this.Handler();
            return this.RenderChildren();
        }
    }
}