using Mjml.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core
{
    public abstract class BaseComponent : IComponent
    {
        public XElement Element { get; set; }

        public List<IComponent> Children { get; set; } = new List<IComponent>();

        public Dictionary<string, string> Attributes { get; set; }

        public string GetTagName()
        {
            throw new NotImplementedException();
        }

        public bool IsRawElement()
        {
            throw new NotImplementedException();
        }

        public string GetCssAttribute(string attributeName)
        {
            var attribute = this.Attributes.FirstOrDefault(a => a.Key == attributeName);
            return !string.IsNullOrWhiteSpace(attribute.Value) ? $"{attribute.Key}: {attribute.Value};" : string.Empty;
        }

        public string GetAttribute(string attributeName)
        {
            var attribute = this.Attributes.FirstOrDefault(a => a.Key == attributeName);
            return attribute.Value;
        }

        public string GetContent()
        {
            return Element.Value;
        }

        public virtual string RenderChildren()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var childComponent in Children)
            {
                sb.Append(childComponent.RenderMjml());
            }

            return sb.ToString();
        }

        public virtual string RenderMjml()
        {
            return $@"
            <MjmlComponent elementType=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </MjmlComponent>";
        }

        public void SetAttributes()
        {
            var attributes = Element.Attributes();

            foreach (var attribute in attributes)
            {
                string userAttributeName = attribute.Name.LocalName.ToLowerInvariant();
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

        public BaseComponent(XElement element)
        {
            Element = element;

            // LR: Sets the Allowed attributes along with the default values.
            Attributes = SetAllowedAttributes();

            if (Element.HasAttributes)
                SetAttributes();
        }
    }
}