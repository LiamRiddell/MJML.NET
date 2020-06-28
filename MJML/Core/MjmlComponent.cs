using Mjml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core
{
    public abstract class MjmlComponent : IMjmlComponent
    {
        public XElement Element { get; set; }

        public List<IMjmlComponent> Children { get; set; } = new List<IMjmlComponent>();

        public virtual Dictionary<string, string> DefaultAttributes { get; set; } = new Dictionary<string, string>();

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
            var attribute = this.DefaultAttributes.FirstOrDefault(a => a.Key == attributeName);
            return !string.IsNullOrWhiteSpace(attribute.Value) ? $"{attribute.Key}: {attribute.Value}" : string.Empty;
        }

        public string GetAttribute(string attributeName)
        {
            var attribute = this.DefaultAttributes.FirstOrDefault(a => a.Key == attributeName);
            return attribute.Value;
        }

        public string GetContent()
        {
            throw new NotImplementedException();
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
                if (!DefaultAttributes.ContainsKey(userAttributeName))
                    continue;

                // TODO: Validate the users input
                DefaultAttributes[userAttributeName] = userAttributeValue;
            }
        }

        public MjmlComponent(XElement element)
        {
            Element = element;

            if (Element.HasAttributes)
                SetAttributes();
        }
    }
}