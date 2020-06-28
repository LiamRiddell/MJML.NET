using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlSectionComponent : MjmlBodyComponent
    {
        public MjmlSectionComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string> {
                { "background-color", "red" }
            };
        }

        public override string RenderMjml()
        {
            return $@"
            <MjmlSectionComponent type=""{Element.Name.LocalName}"" style=""{this.GetCssAttribute("background-color")}"">
                {this.RenderChildren()}
            </MjmlSectionComponent>";
        }
    }
}