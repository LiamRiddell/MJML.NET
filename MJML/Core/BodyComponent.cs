using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core
{
    public abstract class BodyComponent : BaseComponent
    {
        public CssStyleLibraries StyleLibraries { get; set; } = new CssStyleLibraries();

        public BodyComponent(XElement element) : base(element)
        {
            SetupStyles();
        }

        public virtual void SetupStyles()
        {
        }

        public string HtmlAttributes(Dictionary<string, string> htmlAttributes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> htmlAttributePair in htmlAttributes)
            {
                string value = htmlAttributePair.Value;

                // LR: Handle Special Attributes
                if (htmlAttributePair.Key.Equals("style", StringComparison.InvariantCultureIgnoreCase))
                {
                    // LR: Overwrite value
                    value = Styles(htmlAttributePair.Value);
                }

                // LR: Append attribute in html format
                if (!string.IsNullOrWhiteSpace(value))
                {
                    sb.Append($" {htmlAttributePair.Key}=\"{value}\"");
                }
            }

            return sb.ToString().Trim();
        }

        public string Styles(string styleLibraryName)
        {
            // LR: Get the style library
            var styleLibrary = this.StyleLibraries.GetStyleLibrary(styleLibraryName);

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> cssPropertyPair in styleLibrary)
            {
                string value = cssPropertyPair.Value;

                // LR: Append attribute in css format
                if (!string.IsNullOrWhiteSpace(value))
                {
                    sb.Append($"{cssPropertyPair.Key}:{cssPropertyPair.Value};");
                }
            }

            return sb.ToString().Trim();
        }
    }
}