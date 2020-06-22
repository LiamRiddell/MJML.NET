using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlTextComponent : MjmlBaseComponent
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