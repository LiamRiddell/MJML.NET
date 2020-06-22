using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MJML
{
    public class MjmlRawComponent : MjmlBaseComponent
    {
        public MjmlRawComponent(XElement element) : base(element)
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