using Mjml.Core;
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

        Dictionary<string, string> DefaultAttributes { get; set; }

        string GetTagName();

        bool IsRawElement();

        string GetCssAttribute(string attributeName);

        string GetAttribute(string attributeName);

        string GetContent();

        string RenderChildren();

        string RenderMjml();

        void SetAttributes();
    }
}