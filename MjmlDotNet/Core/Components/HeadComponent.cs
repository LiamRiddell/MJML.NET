using AngleSharp.Dom;
using MjmlDotNet.Components.Mjml;
using System;

namespace MjmlDotNet.Core.Components
{
    internal class HeadComponent : BaseComponent
    {
        public MjmlRootComponent VirtualDocument { get; set; }

        public HeadComponent(IElement element, BaseComponent parent, MjmlRootComponent documentRoot = null) : base(element, parent)
        {
            VirtualDocument = documentRoot;
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