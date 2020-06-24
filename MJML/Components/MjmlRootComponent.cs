using Mjml.Core;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlRootComponent : MjmlBaseComponent
    {
        public MjmlRootComponent(XElement element) : base(element)
        {
        }

        public override string RenderMjml()
        {
            return $@"
            <Mjml type=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </Mjml>";
        }
    }
}