using AngleSharp.Dom;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Css;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Html
{
    /// <summary>
    /// HtmlText outputs the text content of html node.
    /// </summary>
    internal class HtmlTextComponent : BodyComponent
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