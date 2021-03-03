using AngleSharp.Dom;
using MjmlDotNet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Core.Components
{
    internal abstract class BaseComponent : IComponent
    {
        public IElement Element { get; set; }

        public BaseComponent Parent { get; set; } = null;

        public List<IComponent> Children { get; set; } = new List<IComponent>();

        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public string GetTagName()
        {
            return Element.NodeName.ToLower();
        }

        public virtual bool IsRawElement()
        {
            throw new NotImplementedException();
        }

        public string GetAttributeToCss(string attributeName)
        {
            var attribute = this.Attributes.FirstOrDefault(a => a.Key == attributeName);
            return !string.IsNullOrWhiteSpace(attribute.Value) ? $"{attribute.Key}: {attribute.Value};" : string.Empty;
        }

        public string GetAttribute(string attributeName)
        {
            var attribute = this.Attributes.FirstOrDefault(a => a.Key == attributeName);
            return attribute.Value;
        }

        public bool HasAttribute(string attributeName)
        {
            // LR: Key Exists && Value != string.Empty
            return this.Attributes.ContainsKey(attributeName) ? !string.IsNullOrWhiteSpace(GetAttribute(attributeName)) : false;
        }

        public string GetContent()
        {
            return string.IsNullOrWhiteSpace(Element.NodeValue) ? Element.TextContent : Element.NodeValue;
        }

        public virtual string RenderChildren()
        {
            if (!this.Children.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var childComponent in Children)
            {
                sb.Append(childComponent.RenderMjml());
            }

            return sb.ToString();
        }

        public virtual string RenderMjml()
        {
            return this.RenderChildren();
        }

        public virtual void SetAttributes()
        {
            var attributes = (Element as IElement).Attributes;

            foreach (var attribute in attributes)
            {
                string userAttributeName = attribute.Name.ToLowerInvariant();
                string userAttributeValue = attribute.Value;

                // LR: Validate the attribute exists in the DefaultAttributes
                if (!Attributes.ContainsKey(userAttributeName))
                    continue;

                // TODO: Validate the users input
                Attributes[userAttributeName] = userAttributeValue;
            }
        }

        public virtual Dictionary<string, string> SetAllowedAttributes()
        {
            throw new NotImplementedException();
        }

        public bool HasParentComponent()
        {
            return Parent != null;
        }

        public BaseComponent GetParentComponent()
        {
            if (!HasParentComponent())
                throw new NullReferenceException();

            return Parent;
        }

        public void SetAttribute(string attributeName, string attributeValue)
        {
            if (string.IsNullOrWhiteSpace(attributeName) || !HasAttribute(attributeName))
                return;

            Attributes[attributeName] = attributeValue;
        }

        public void AddAttributeRaw(string attributeName, string attributeValue)
        {
            if (HasAttribute(attributeName))
                return;

            Attributes.Add(attributeName, attributeValue);
        }

        public bool IsHeadComponent()
        {
            return this is HeadComponent;
        }

        public bool IsBodyComponent()
        {
            return this is BodyComponent;
        }

        public void SetupComponentAttributes()
        {
            // LR: Sets the Allowed attributes along with the default values.
            // NOTE: This creates a copy of the dictionary opposed to referencing the original
            Attributes = new Dictionary<string, string>(SetAllowedAttributes());

            if (Element.Attributes.Any())
                SetAttributes();
        }

        public BaseComponent(IElement element, BaseComponent parent)
        {
            Element = element;
            Parent = parent;
            SetupComponentAttributes();
        }
    }
}