using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlTextComponent : MjmlBodyComponent
    {
        public MjmlTextComponent(XElement element) : base(element)
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
            return $@"
            <MjmlRawComponent type=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </MjmlRawComponent>";
        }
    }
}