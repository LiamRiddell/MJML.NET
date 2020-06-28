using Mjml.Core;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlRootComponent : MjmlBodyComponent
    {
        public MjmlRootComponent(XElement element) : base(element)
        {
        }

        public override string RenderMjml()
        {
            return $@"
            <MjmlRootComponent type=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </MjmlRootComponent>";
        }
    }
}