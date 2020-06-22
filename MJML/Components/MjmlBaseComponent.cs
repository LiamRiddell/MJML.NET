using Mjml.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Components
{
    public abstract class MjmlBaseComponent : IMjmlComponent
    {
        public XElement Element { get; set; }

        public List<IMjmlComponent> Children { get; set; }

        public virtual string RenderMjml()
        {
            throw new NotImplementedException();
        }

        public virtual string RenderChildren()
        {
            var output = string.Empty;

            foreach (var childComponent in Children)
            {
                output += childComponent.RenderMjml();
            }

            return output;
        }

        public MjmlBaseComponent(XElement element)
        {
            Element = element;
            Children = new List<IMjmlComponent>();

            // TODO: Load attributes
        }

        //constructor();
        //getTagName();
        //isRawElement();
        //getChildContext();
        //getAttribute(name);
        //getContent();
        //renderMjml();
    }
}