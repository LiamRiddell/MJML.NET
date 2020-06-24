using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core
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