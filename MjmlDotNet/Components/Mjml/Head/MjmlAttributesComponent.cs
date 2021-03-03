using AngleSharp.Dom;
using MjmlDotNet.Components.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MjmlDotNet.Components.Mjml.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-style/src/index.js
    internal class MjmlAttributesComponent : HeadComponent
    {
        private List<string> _modifiedComponents = new List<string>();

        public MjmlAttributesComponent(IElement element, BaseComponent parent, MjmlRootComponent documentRoot) : base(element, parent, documentRoot)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.NoAttributes;
        }

        public override void Handler()
        {
            // LR: Update the global default attributes
            foreach (var child in Children.Where( c => c.IsBodyComponent() ))
            {
                if (UpdateDefaultAttributes(child as BodyComponent))
                {
                    _modifiedComponents.Add(child.GetTagName());
                }
            }

            // LR: Rebuild all component styles
            if (VirtualDocument != null)
            {
                RebuildComponentAttributesRecursive(VirtualDocument);
            }
        }

        public void RebuildComponentAttributesRecursive(BaseComponent component)
        {
            if (component.IsHeadComponent())
                return;

            foreach (var child in component?.Children)
            {
                if (_modifiedComponents.Contains(child.GetTagName()) && child.IsBodyComponent())
                {
                    // Rebuild the components attributes
                    child.SetupComponentAttributes();

                    // Rebuild the components styles based on newly update attributes
                    (child as BodyComponent).SetupStyles();
                }

                if (child.Children.Any())
                    RebuildComponentAttributesRecursive(child as BaseComponent);
            }
        }

        public bool UpdateDefaultAttributes(BodyComponent component)
        {
            if (!component.Element.Attributes.Any())
                return false;

            var tagName = component.GetTagName().ToLowerInvariant();

            // LR: Currently Unsupported
            if (tagName.Equals("mj-all") || tagName.Equals("mj-class"))
                return false;

            var defaultAttributes = GetDefaultAttributesFromTagName(tagName);

            // LR: Empty Default Allowed Attributes
            if (!defaultAttributes.Any())
                return false;

            // LR: Override existing default values with the defined attributes
            bool defaultStylesModified = false;
            foreach (var attribute in component.Element.Attributes)
            {
                if (defaultAttributes.ContainsKey(attribute.Name))
                {
                    defaultAttributes[attribute.Name] = attribute.Value;
                    defaultStylesModified = true;
                } else
                {
                    // Attribute not allowed.
                }
            }

            return defaultStylesModified;
        }

        public Dictionary<string, string> GetDefaultAttributesFromTagName(string tagName)
        {
            switch (tagName)
            {
                case "mj-breakpoint":
                    return GlobalDefaultAttributes.Head.MjmlBreakpoint;

                case "mj-font":
                    return GlobalDefaultAttributes.Head.MjmlFont;

                case "mj-style":
                    return GlobalDefaultAttributes.Head.MjmlStyle;

                case "mj-body":
                    return GlobalDefaultAttributes.Body.MjmlBody;

                case "mj-section":
                    return GlobalDefaultAttributes.Body.MjmlSection;

                case "mj-group":
                    return GlobalDefaultAttributes.Body.MjmlGroup;

                case "mj-column":
                    return GlobalDefaultAttributes.Body.MjmlColumn;

                case "mj-text":
                    return GlobalDefaultAttributes.Body.MjmlText;

                case "mj-spacer":
                    return GlobalDefaultAttributes.Body.MjmlSpacer;

                case "mj-divider":
                    return GlobalDefaultAttributes.Body.MjmlDivider;

                case "mj-image":
                    return GlobalDefaultAttributes.Body.MjmlImage;

                case "mj-button":
                    return GlobalDefaultAttributes.Body.MjmlButton;

                case "mj-hero":
                    return GlobalDefaultAttributes.Body.MjmlHero;

                case "mj-social":
                    return GlobalDefaultAttributes.Body.MjmlSocial;

                case "mj-social-element":
                    return GlobalDefaultAttributes.Body.MjmlSocialElement;

                default:
                    return new Dictionary<string, string>();
            }
        }

        // LR: Omit the child components
        public override string RenderMjml()
        {
            this.Handler();
            return string.Empty;
        }
    }
}