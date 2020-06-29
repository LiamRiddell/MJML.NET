using Mjml.Components;
using Mjml.Helpers;
using Mjml.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            HtmlSkeleton htmlSkeleton = new HtmlSkeleton();

            // return htmlSkeleton.Build(VirtualDocument.RenderMjml());
            return VirtualDocument.RenderMjml();
        }

        #endregion Public

        #region Private

        private IMjmlComponent CreateMjmlComponent(XElement element)
        {
            string elementTag = element.Name.LocalName.ToLowerInvariant();

            switch (elementTag)
            {
                case "mjml":
                    return new MjmlRootComponent(element);

                case "mj-section":
                    return new MjmlSectionComponent(element);

                case "mj-text":
                    return new MjmlTextComponent(element);

                case "html-text":
                    return new HtmlTextComponent(element);

                default:
                    return new HtmlRawComponent(element);
            }
        }

        private void GenerateVirtualDocument(XElement element)
        {
            VirtualDocument = CreateMjmlComponent(element) as MjmlRootComponent;

            if (VirtualDocument.Element.IsEmpty)
                return;

            TraverseElementTree(VirtualDocument.Element, VirtualDocument);
        }

        private void TraverseElementTree(XElement element, IMjmlComponent parentComponent)
        {
            if (element.IsEmpty)
                return;

            // LR: Traverse the children
            foreach (var childElement in element.Nodes())
            {
                IMjmlComponent childComponent;

                if (childElement.NodeType == XmlNodeType.Element)
                {
                    childComponent = CreateMjmlComponent((XElement)childElement);
                    parentComponent.Children.Add(childComponent);
                    TraverseElementTree((XElement)childElement, childComponent);
                }
                else if (childElement.NodeType == XmlNodeType.Text)
                {
                    var childElementText = childElement as XText;

                    if (string.IsNullOrEmpty(childElementText.Value) || string.IsNullOrWhiteSpace(childElementText.Value))
                        continue;

                    var childXElement = new XElement("html-text", childElementText.Value);
                    childXElement.Name = XName.Get("html-text");

                    childComponent = CreateMjmlComponent(childXElement);
                    parentComponent.Children.Add(childComponent);
                }
            }
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