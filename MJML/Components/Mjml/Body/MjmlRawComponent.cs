using AngleSharp.Dom;
using MjmlDotNet.Core.Component;
using System.Collections.Generic;
using System.Linq;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-raw/src/index.js
    public class MjmlRawComponent : BodyComponent
    {
        public MjmlRawComponent(IElement element, BaseComponent parent) : base(element, parent)
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

            return Element.ToString();
        }

        public override string RenderMjml()
        {
            return this.RenderChildren();
        }
    }
}