using AngleSharp.Dom;
using MjmlDotNet.Core.Components;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml
{
    public class MjmlRootComponent : BodyComponent
    {
        public MjmlRootComponent(IElement element, BaseComponent parent) : base(element, parent)
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