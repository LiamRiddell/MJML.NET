using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlSectionComponent : MjmlBodyComponent
    {
        public override Dictionary<string, string> DefaultAttributes { get; set; } = new Dictionary<string, string>()
        {
            { "background-color", "red" },
        };

        public MjmlSectionComponent(XElement element) : base(element)
        {
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