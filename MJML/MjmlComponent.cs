using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MJML
{
    public class MjmlComponent
    {
        public XElement Element { get; set; }
        public List<MjmlComponent> Children { get; set; }

        public MjmlComponent(XElement element)
        {
            Element = element;
            Children = new List<MjmlComponent>();
        }

        public virtual void RenderMjml()
        {
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