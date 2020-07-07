using Mjml.Core;
using Mjml.Core.Component;
using Mjml.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents.Body
{
    public class MjmlBodyComponent : BodyComponent
    {
        public MjmlBodyComponent(XElement element) : base(element)
        {
            if (HasAttribute("Width"))
                HtmlSkeleton.ContainerWidth = GetAttribute("width");

            if (HasAttribute("background-color"))
                HtmlSkeleton.BackgroundColor = GetAttribute("background-color");
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
                { "width", "600px" },
                { "background-color", string.Empty }
            };
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "background-color", GetAttribute("background-color") },
            });
        }

        public override string RenderMjml()
        {
            return $@"
                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "class", GetAttribute("css-class") },
                        { "style", "div" }
                     })}
                >
                    {this.RenderChildren()}
                </div>
            ";
        }
    }
}