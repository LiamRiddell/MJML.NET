using Mjml.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core
{
    public abstract class MjmlBodyComponent : MjmlComponent
    {
        public MjmlBodyComponent(XElement element) : base(element)
        {
        }

        public virtual Dictionary<string, string> GetStyles()
        {
            return new Dictionary<string, string>();
        }
    }
}