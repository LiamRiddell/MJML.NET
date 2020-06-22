using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Interfaces
{
    public interface IMjmlComponent
    {
        XElement Element { get; set; }

        List<IMjmlComponent> Children { get; set; }

        string RenderChildren();

        string RenderMjml();
    }
}