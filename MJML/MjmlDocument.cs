using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Mjml.Core.Component;
using Mjml.Helpers;
using Mjml.HtmlComponents;
using Mjml.MjmlComponents;
using Mjml.MjmlComponents.Body;
using Mjml.MjmlComponents.Head;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Mjml
{
    public class MjmlDocument
    {
        /// <summary>
        /// AngleSharp document used for traversing the mjml template
        /// </summary>
        private IDocument _document { get; set; }

        private IHtmlParser _htmlParser { get; set; }

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
            // var context = BrowsingContext.New(Configuration.Default);
            // Create a document from a virtual request / response pattern
            // _document = context.OpenAsync(req => req.Content(content)).GetAwaiter().GetResult();

            // LR: AngleSharp HtmlParser
            _htmlParser = new HtmlParser();

            // HACK: Unknown self-closing tags break AngleSharps DOM. Any siblings after unknown self-closing element becomes a child of the unkown element.
            // We use regex to find and close the self-closing tags for mjml
            Regex selfClosingMjml = new Regex(@"(<mj-.*\/>)", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline);

            content = selfClosingMjml.Replace(content, delegate (Match m)
            {
                var tagName = m.Value.Substring(1, m.Value.IndexOf(' ') - 1);
                return $"{m.Value.Replace("/>", "")}></{tagName}>";
            });

            // LR: Parse mjml document
            _document = _htmlParser.ParseDocument(content);
        }

        #region Public

        public bool Parse()
        {
            // LR: Parse the XML document
            if (!_document.All.Any())
                return false;

            GenerateVirtualDocument();

            return true;
        }

        public string Render(bool prettify = false)
        {
            string html = HtmlSkeleton.Build(VirtualDocument.RenderMjml());

            return prettify ? PrettifyHtml(html) : html;
        }

        #endregion Public

        #region Private

        private BaseComponent CreateMjmlComponent(IElement element, BaseComponent parent = null)
        {
            string elementTag = element.NodeName.ToLowerInvariant();

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

                case "mj-group":
                    return new MjmlGroupComponent(element, parent);

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

        private void GenerateVirtualDocument()
        {
            var mjmlRoot = _document.QuerySelector<IElement>("mjml");

            if (mjmlRoot == null)
                throw new NullReferenceException();

            // LR: Create the root element for the virtual DOM
            VirtualDocument = CreateMjmlComponent(mjmlRoot) as MjmlRootComponent;

            if (!VirtualDocument.Element.Descendents<IElement>().Any())
                return;

            TraverseElementTree(VirtualDocument.Element, VirtualDocument);
        }

        private void TraverseElementTree(INode element, BaseComponent parentComponent)
        {
            Console.WriteLine($"Traversing <{element.NodeName.ToLowerInvariant()}>");

            if (!element.ChildNodes.Any())
                return;

            // LR: Traverse the children
            foreach (var childElement in element.ChildNodes)
            {
                BaseComponent childComponent;

                if (childElement.NodeType == NodeType.Element)
                {
                    // LR: Create MJML component
                    childComponent = CreateMjmlComponent(childElement as Element, parentComponent);

                    // LR: Add child component to parent
                    parentComponent.Children.Add(childComponent);

                    // LR: Traverse the child element and change the parent context
                    TraverseElementTree(childElement, childComponent);
                }
                else if (childElement.NodeType == NodeType.Text)
                {
                    var childElementText = childElement as IText;

                    if (string.IsNullOrWhiteSpace(childElementText.NodeValue))
                        continue;

                    // HACK: Convert raw-text to element - this prevents text with no parent tag being lost
                    var textElement = _document.CreateElement("html-text");
                    textElement.NodeValue = childElement.TextContent;
                    textElement.TextContent = childElement.TextContent;

                    // LR: Create MJML component
                    childComponent = CreateMjmlComponent(textElement, parentComponent);

                    // LR: Add child component to parent
                    parentComponent.Children.Add(childComponent);
                }
            }
        }

        private string PrettifyHtml(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;

            // LR: Parse the document using AngleSharp
            var document = _htmlParser.ParseDocument(content);

            return document.Prettify();
        }

        private string MinifyHtml(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;

            // LR: Parse the document using AngleSharp
            var document = _htmlParser.ParseDocument(content);

            return document.Minify();
        }

        #endregion Private
    }
}