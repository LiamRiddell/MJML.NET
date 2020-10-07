using AngleSharp.Dom;
using Mjml.Core.Component;
using Mjml.Core.Css;
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

        public override CssBoxModel GetBoxModel()
        {
            return new CssBoxModel(0, 0, 0, 0);
        }

        public override string RenderMjml()
        {
            return Element.Text();
        }
    }
}