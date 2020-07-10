﻿using AngleSharp.Dom;
using Mjml.Core.Component;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Core.Interfaces
{
    public interface IComponent
    {
        IElement Element { get; set; }

        BaseComponent Parent { get; set; }

        List<IComponent> Children { get; set; }

        Dictionary<string, string> Attributes { get; set; }

        Dictionary<string, string> SetAllowedAttributes();

        string GetTagName();

        bool IsRawElement();

        string GetAttributeToCss(string attributeName);

        string GetAttribute(string attributeName);

        bool HasAttribute(string attributeName);

        string GetContent();

        string RenderChildren();

        string RenderMjml();

        void SetAttributes();

        void SetAttribute(string attributeName, string attributeValue);

        bool HasParentComponent();

        BaseComponent GetParentComponent();
    }
}