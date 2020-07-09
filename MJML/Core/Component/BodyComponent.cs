using AngleSharp.Dom;
using Mjml.Core.Css;
using Mjml.Core.Interfaces;
using Mjml.Extensions;
using Mjml.Helpers;
using Mjml.HtmlComponents;
using Mjml.MjmlComponents.Body;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Mjml.Core.Component
{
    public abstract class BodyComponent : BaseComponent
    {
        public CssBoxModel CssBoxModel { get; set; }
        public CssStyleLibraries StyleLibraries { get; set; } = new CssStyleLibraries();

        public BodyComponent(Element element, BaseComponent parent) : base(element, parent)
        {
            string tagName = GetTagName();

            // LR: Setup Box Model
            CssBoxModel = GetBoxModel();

            // LR: Register component styles
            HtmlSkeleton.AddHeadStyle(tagName, HeadStyle());
            HtmlSkeleton.AddComponentHeadStyle(tagName, ComponentsHeadStyle());

            // LR: Setup last
            SetupStyles();
        }

        public virtual void SetupStyles()
        {
        }

        public string HtmlAttributes(Dictionary<string, string> htmlAttributes, bool mergeDefaultCSSProperties = false)
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
                        value = Styles(htmlAttributePair.Value, mergeDefaultCSSProperties);
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

        public string Styles(string styleLibraryName, bool outputDefaults = false)
        {
            // LR: Load the style library
            Dictionary<string, string> styleLibrary = this.StyleLibraries.GetStyleLibrary(styleLibraryName);

            if (!outputDefaults)
                return InlineCss(styleLibrary);

            // LR: Default CSS Component Properties
            Dictionary<string, string> defaultStyles = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> attribute in Attributes)
            {
                if (string.IsNullOrWhiteSpace(attribute.Value))
                    continue;

                if (!CssHelper.IsCssProperty(attribute.Key))
                {
                    Console.WriteLine($"[IsCssProperty] => Omitted {attribute.Key} as it's not a CssProperty");
                    continue;
                }

                defaultStyles.Add(attribute.Key, attribute.Value);
            }

            // LR: Merge style library into default styles.
            // CAUTION: This will include unessary properties to be outputted to CSS.
            return InlineCss(defaultStyles.MergeLeft(styleLibrary));
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

        public float GetShorthandBorderValue(string direction)
        {
            string mjAttributeDirection = GetAttribute($"border-{direction}");
            string mjAttribute = GetAttribute("border");

            if (!string.IsNullOrWhiteSpace(mjAttributeDirection))
                return CssUnitParser.Parse(mjAttributeDirection).Value;

            if (string.IsNullOrWhiteSpace(mjAttribute))
                return 0;

            // MERGED borderParser: https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/helpers/shorthandParser.js#L3
            //return CssUnitParser.Parse(mjAttribute).Value;
            Regex regex = new Regex("(?:(?:^| )([0-9]+))");
            var match = regex.Match(mjAttribute);

            if (!match.Success)
                return 0;

            return float.Parse(match.Value.Trim());
        }

        // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/createComponent.js#L115
        public virtual CssBoxModel GetBoxModel()
        {
            // LR: Default to the outmost container
            CssParsedUnit containerWidth = CssUnitParser.Parse(HtmlSkeleton.ContainerWidth);

            var paddings =
                GetShorthandAttributeValue("padding", "right") +
                GetShorthandAttributeValue("padding", "left");

            var borders =
                GetShorthandBorderValue("right") +
                GetShorthandBorderValue("left");

            // LR: Try and get the parents calculated Box Model size.
            if (HasParentComponent())
            {
                var parent = GetParentComponent() as BodyComponent;

                // LR: Calculate based of the parents inner width (width after removing paddings and borders)
                containerWidth.Value = parent.GetContainerInnerWidth() - paddings - borders;

                return new CssBoxModel(
                    parent.GetContainerInnerWidth(),
                    borders,
                    paddings,
                    containerWidth.Value
                );
            }

            return new CssBoxModel(
                containerWidth.Value,
                borders,
                paddings,
                containerWidth.Value);
        }

        public virtual string HeadStyle()
        {
            return string.Empty;
        }

        public virtual string ComponentsHeadStyle()
        {
            return string.Empty;
        }

        public override bool IsRawElement()
        {
            return
                this is MjmlRawComponent ||
                this is HtmlRawComponent ||
                this is HtmlTextComponent;
        }

        public bool IsContainerElement()
        {
            return this is MjmlBodyComponent || this is MjmlWrapperComponent || (this is MjmlSectionComponent && Parent is MjmlBodyComponent);
        }

        public float GetContainerInnerWidth()
        {
            return CssBoxModel.BoxWidth;
        }

        public float GetContainerOuterWidth()
        {
            return CssBoxModel.TotalWidth;
        }
    }
}