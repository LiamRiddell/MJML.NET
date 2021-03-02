using AngleSharp.Dom;
using System;

namespace MjmlDotNet.Core.Components
{
    internal class HeadComponent : BaseComponent
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