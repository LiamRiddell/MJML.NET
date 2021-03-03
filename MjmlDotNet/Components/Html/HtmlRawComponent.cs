using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Html
{
    /// <summary>
    /// HtmlText outputs the text content of html node.
    /// </summary>
    internal class HtmlRawComponent : BodyComponent
    {
        public HtmlRawComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.NoAttributes;
        }

        /// <summary>
        /// Passes the original html tag attributes to output.
        /// </summary>
        public override void SetAttributes()
        {
            var attributes = Element.Attributes;

            foreach (var attribute in attributes)
            {
                Attributes.Add(attribute.Name, attribute.Value);
            }
        }

        public override string RenderMjml()
        {
            return $@"
            <{Element.NodeName} {HtmlAttributes(Attributes)}>
                {this.RenderChildren()}
            </{Element.NodeName}>";
        }
    }
}