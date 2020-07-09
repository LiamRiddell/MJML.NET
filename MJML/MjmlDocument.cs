using Mjml.Core.Component;
using Mjml.Helpers;
using Mjml.HtmlComponents;
using Mjml.MjmlComponents;
using Mjml.MjmlComponents.Body;
using Mjml.MjmlComponents.Head;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Xml;
using System.Xml.Linq;

namespace Mjml
{
    public class MjmlDocument
    {
        /// <summary>
        /// XDocument used for traversing the mjml template
        /// </summary>
        private XDocument _document { get; set; }

        /// <summary>
        /// Root element containg all child components
        /// </summary>
        public MjmlRootComponent VirtualDocument { get; set; }

        /// <summary>
        /// Load the content into string reader and then create XDocument
        /// </summary>
        /// <param name="content"></param>
        public MjmlDocument(string content)
        {
            using (StringReader sr = new StringReader(content))
            using (XmlReader reader = XmlReader.Create(sr))
            {
                _document = XDocument.Load(reader);
            }
        }

        #region Public

        public bool Parse()
        {
            // LR: Parse the XML document
            if (_document.Root.IsEmpty)
                return false;

            GenerateVirtualDocument(_document.Root);

            return true;
        }

        public string Render()
        {
            return HtmlSkeleton.Build(VirtualDocument.RenderMjml());
        }

        #endregion Public

        #region Private

        private BaseComponent CreateMjmlComponent(XElement element, BaseComponent parent = null)
        {
            string elementTag = element.Name.LocalName.ToLowerInvariant();

            switch (elementTag)
            {
                case "mjml":
                    return new MjmlRootComponent(element, parent);

                case "mj-head":
                    return new MjmlHeadComponent(element, parent);

                case "mj-title":
                    return new MjmlTitleComponent(element, parent);

                case "mj-preview":
                    return new MjmlPreviewComponent(element, parent);

                case "mj-breakpoint":
                    return new MjmlBreakpointComponent(element, parent);

                case "mj-font":
                    return new MjmlFontComponent(element, parent);

                case "mj-style":
                    return new MjmlStyleComponent(element, parent);

                case "mj-body":
                    return new MjmlBodyComponent(element, parent);

                case "mj-wrapper":
                    return new MjmlWrapperComponent(element, parent);

                case "mj-section":
                    return new MjmlSectionComponent(element, parent);

                case "mj-column":
                    return new MjmlColumnComponent(element, parent);

                case "mj-text":
                    return new MjmlTextComponent(element, parent);

                case "mj-spacer":
                    return new MjmlSpacerComponent(element, parent);

                case "mj-raw":
                    return new MjmlRawComponent(element, parent);

                case "mj-image":
                    return new MjmlImageComponent(element, parent);

                case "mj-button":
                    return new MjmlButtonComponent(element, parent);

                case "html-text":
                    return new HtmlTextComponent(element, parent);

                default:
                    return new HtmlRawComponent(element, parent);
            }
        }

        private void GenerateVirtualDocument(XElement element)
        {
            VirtualDocument = CreateMjmlComponent(element) as MjmlRootComponent;

            if (VirtualDocument.Element.IsEmpty)
                return;

            TraverseElementTree(VirtualDocument.Element, VirtualDocument);
        }

        private void TraverseElementTree(XElement element, BaseComponent parentComponent)
        {
            if (element.IsEmpty)
                return;

            // LR: Traverse the children
            foreach (var childElement in element.Nodes())
            {
                BaseComponent childComponent;

                if (childElement.NodeType == XmlNodeType.Element)
                {
                    // LR: Create MJML component
                    childComponent = CreateMjmlComponent((XElement)childElement, parentComponent);

                    // LR: Add child component to parent
                    parentComponent.Children.Add(childComponent);

                    // LR: Traverse the child element and change the parent context
                    TraverseElementTree((XElement)childElement, childComponent);
                }
                else if (childElement.NodeType == XmlNodeType.Text)
                {
                    var childElementText = childElement as XText;

                    if (string.IsNullOrEmpty(childElementText.Value) || string.IsNullOrWhiteSpace(childElementText.Value))
                        continue;

                    var childXElement = new XElement("html-text", childElementText.Value);
                    childXElement.Name = XName.Get("html-text");

                    // LR: Create MJML component
                    childComponent = CreateMjmlComponent(childXElement, parentComponent);

                    // LR: Add child component to parent
                    parentComponent.Children.Add(childComponent);
                }
            }
        }

        private string EscapeXmlContent(string xmlContent)
        {
            // LR: find all attributes

            //XmlDocument doc = new XmlDocument();
            //return doc.CreateTextNode(xmlContent).OuterXml;

            return SecurityElement.Escape(xmlContent);
        }

        #endregion Private

        private void PrintElementType(XElement element)
        {
            switch (element.NodeType)
            {
                case XmlNodeType.Element:
                    Console.WriteLine("<{0}> : Attributes {1} : Children {2}", element.Name, element.Attributes().Count(), element.Descendants().Count());
                    break;

                case XmlNodeType.Text:
                    Console.WriteLine(element.Value);
                    break;

                case XmlNodeType.CDATA:
                    Console.WriteLine("<![CDATA[{0}]]>", element.Value);
                    break;

                case XmlNodeType.ProcessingInstruction:
                    Console.WriteLine("<?{0} {1}?>", element.Name, element.Value);
                    break;

                case XmlNodeType.Comment:
                    Console.WriteLine("<!--{0}-->", element.Value);
                    break;

                case XmlNodeType.XmlDeclaration:
                    Console.WriteLine("<?xml version='1.0'?>");
                    break;

                case XmlNodeType.Document:
                    break;

                case XmlNodeType.DocumentType:
                    Console.WriteLine("<!DOCTYPE {0} [{1}]", element.Name, element.Value);
                    break;

                case XmlNodeType.EntityReference:
                    Console.WriteLine(element.Name);
                    break;

                case XmlNodeType.EndElement:
                    Console.WriteLine("</{0}>", element.Name);
                    break;
            }
        }
    }
}