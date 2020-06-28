using Mjml.Core;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlTextComponent : MjmlBodyComponent
    {
        public MjmlTextComponent(XElement element) : base(element)
        {
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