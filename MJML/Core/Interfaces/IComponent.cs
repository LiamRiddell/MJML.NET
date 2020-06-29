using Mjml.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core.Interfaces
{
    public interface IComponent
    {
        XElement Element { get; set; }
        List<IComponent> Children { get; set; }

        Dictionary<string, string> Attributes { get; set; }

        Dictionary<string, string> SetAllowedAttributes();

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