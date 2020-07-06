using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.MjmlComponents
{
    public class MjmlBodyComponent : BodyComponent
    {
        public MjmlBodyComponent(XElement element) : base(element)
        {
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
            string backgroundColor = GetAttribute("background-color");
            // TODO: setBackgroundColor(this.getAttribute('background-color'))

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