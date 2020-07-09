using AngleSharp.Dom;
using Mjml.Core.Interfaces;
using System;
using System.Xml.Linq;

namespace Mjml.Core.Component
{
    public class HeadComponent : BaseComponent
    {
        public HeadComponent(IElement element, BaseComponent parent) : base(element, parent)
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