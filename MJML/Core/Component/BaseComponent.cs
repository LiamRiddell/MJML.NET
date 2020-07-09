using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Mjml.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core.Component
{
    public abstract class BaseComponent : IComponent
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
            return Element.NodeValue;
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

        public BaseComponent(IElement element, BaseComponent parent)
        {
            Element = element;
            Parent = parent;

            // LR: Sets the Allowed attributes along with the default values.
            Attributes = SetAllowedAttributes();

            if (Element.Attributes.Any())
                SetAttributes();
        }
    }
}