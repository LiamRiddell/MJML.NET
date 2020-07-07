using Mjml.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mjml.Core
{
    public abstract class BodyComponent : BaseComponent
    {
        public CssBoxModel CssBoxModel { get; set; }
        public CssStyleLibraries StyleLibraries { get; set; } = new CssStyleLibraries();

        public BodyComponent(XElement element) : base(element)
        {
            CssBoxModel = GetBoxModel();

            // LR: Setup last
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
                    // LR: Check if it's inlined css
                    if (!value.Contains(":") && !value.Contains(";"))
                    {
                        // LR: Get the styles from the style library
                        value = Styles(htmlAttributePair.Value);
                    }
                }

                // LR: Append attribute in html format
                if (!string.IsNullOrWhiteSpace(value))
                {
                    sb.Append($" {htmlAttributePair.Key}=\"{value}\"");
                }
            }

            return sb.ToString().Trim();
        }

        public string InlineCss(Dictionary<string, string> cssProperties)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> cssPropertyPair in cssProperties)
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

        public string Styles(string styleLibraryName)
        {
            Dictionary<string, string> styleLibrary = this.StyleLibraries.GetStyleLibrary(styleLibraryName);
            return InlineCss(styleLibrary);
        }

        public float GetShorthandAttributeValue(string attribute, string direction)
        {
            string mjAttributeDirection = GetAttribute($"{attribute}-{direction}");
            string mjAttribute = GetAttribute(attribute);

            if (!string.IsNullOrWhiteSpace(mjAttributeDirection))
                return CssUnitParser.Parse(mjAttributeDirection).Value;

            if (string.IsNullOrWhiteSpace(mjAttribute))
                return 0;

            // MERGED shorthandParser: https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/helpers/shorthandParser.js#L3
            var splittedCssValue = mjAttribute.Split(' ');
            Dictionary<string, int> directions;

            switch (splittedCssValue.Length)
            {
                case 2:
                    directions = new Dictionary<string, int>
                        {
                            { "top", 0 },
                            { "bottom", 0 },
                            { "left", 1 },
                            { "right", 1 }
                        };
                    break;

                case 3:
                    directions = new Dictionary<string, int>
                        {
                            { "top", 0 },
                            { "bottom", 2 },
                            { "left", 1 },
                            { "right", 1 }
                        };
                    break;

                case 4:
                    directions = new Dictionary<string, int>
                        {
                            { "top", 0 },
                            { "bottom", 2 },
                            { "left", 3 },
                            { "right", 1 }
                        };
                    break;

                case 1:
                default:
                    return CssUnitParser.Parse(mjAttribute).Value;
            }

            return CssUnitParser.Parse(splittedCssValue[directions[direction]]).Value;
        }

        // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/createComponent.js#L115
        public CssBoxModel GetBoxModel()
        {
            CssParsedUnit containerWidth = CssUnitParser.Parse(HtmlSkeleton.ContainerWidth);

            var paddings =
                GetShorthandAttributeValue("padding", "right") +
                GetShorthandAttributeValue("padding", "left");

            var borders =
                GetShorthandAttributeValue("border", "right") +
                GetShorthandAttributeValue("border", "left");

            return new CssBoxModel(
                containerWidth.Value,
                borders,
                paddings,
                containerWidth.Value - paddings - borders);
        }
    }
}