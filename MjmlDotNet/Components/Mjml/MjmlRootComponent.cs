using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml
{
    internal class MjmlRootComponent : BodyComponent
    {
        public MjmlRootComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.NoAttributes;
        }

        public override string RenderMjml()
        {
            return RenderChildren();
        }
    }
}