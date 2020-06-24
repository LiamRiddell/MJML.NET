using Mjml.Core;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlHeadComponent : MjmlBaseComponent
    {
        public MjmlHeadComponent(XElement element) : base(element)
        {
        }

        public override string RenderMjml()
        {
            return $@"
            <MjmlHead type=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </MjmlHead>";
        }
    }
}