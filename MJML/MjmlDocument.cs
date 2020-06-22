using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MJML
{
    public class MjmlDocument
    {
        private XDocument _document { get; set; }

        public MjmlComponent VirtualDocument { get; set; }

        public MjmlDocument(string content)
        {
            // LR: Read the content into a string reader
            using (StringReader sr = new StringReader(content))
            using (XmlReader reader = XmlReader.Create(sr))
            {
                // LR: Load the content into the document
                _document = XDocument.Load(reader);
            }
        }

        public bool Parse()
        {
            // LR: Parse the XML document
            if (_document.Root.IsEmpty)
                return false;

            GenerateVirtualDocument(_document.Root);

            return true;
        }

        public void GenerateVirtualDocument(XElement element)
        {
            VirtualDocument = CreateMjmlComponent(element);

            if (VirtualDocument.Element.IsEmpty)
                return;

            TraverseElementTree(VirtualDocument.Element, VirtualDocument);
        }

        public void TraverseElementTree(XElement element, MjmlComponent parentComponent)
        {
            if (element.IsEmpty)
                return;

            // LR: Traverse the children
            foreach (var childElement in element.Elements())
            {
                var childComponent = CreateMjmlComponent(childElement);

                parentComponent.Children.Add(childComponent);

                TraverseElementTree(childElement, childComponent);
            }
        }

        public MjmlComponent CreateMjmlComponent(XElement element)
        {
            var elementTag = element.Name.LocalName.ToLowerInvariant();

            switch (elementTag)
            {
                case "mj-head":
                    Console.WriteLine("Found a head");
                    break;

                case "mj-section":
                    Console.WriteLine("Found a section");
                    break;

                case "mj-column":
                    Console.WriteLine("Found a column");
                    break;

                default:
                    break;
            }

            return new MjmlComponent(element);
        }

        // LR: Util
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