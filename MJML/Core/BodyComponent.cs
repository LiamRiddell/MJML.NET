using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Core
{
    public abstract class BodyComponent : BaseComponent
    {
        public BodyComponent(XElement element) : base(element)
        {
        }

        public virtual Dictionary<string, string> GetStyles()
        {
            return new Dictionary<string, string>();
        }
    }
}