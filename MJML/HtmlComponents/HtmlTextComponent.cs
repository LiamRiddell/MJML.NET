using AngleSharp.Dom;
using Mjml.Core.Component;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.HtmlComponents
{
    /// <summary>
    /// HtmlText outputs the text content of html node.
    /// </summary>
    public class HtmlTextComponent : BodyComponent
    {
        public HtmlTextComponent(IElement element, BaseComponent parent) : base(element, parent)
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
            return Element.Text();
        }
    }
}