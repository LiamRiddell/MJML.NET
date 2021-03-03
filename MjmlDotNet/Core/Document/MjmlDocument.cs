using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using MjmlDotNet.Components.Html;
using MjmlDotNet.Components.Mjml;
using MjmlDotNet.Components.Mjml.Body;
using MjmlDotNet.Components.Mjml.Head;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using MjmlDotNet.Core.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MjmlDotNet.Core.Document
{
    internal class MjmlDocument : IMjmlDocument, IDisposable
    {
        /// <summary>
        /// Options
        /// </summary>
        private readonly MjmlParserOptions _parserOptions;

        /// <summary>
        /// AngleSharp document used for traversing the mjml template
        /// </summary>
        private IDocument _document { get; set; }

        /// <summary>
        /// AngleSharp HtmlParser used for parsing the mjml template
        /// </summary>
        private IHtmlParser _htmlParser { get; set; }

        /// <summary>
        /// Root element containg all document components
        /// </summary>
        private MjmlRootComponent VirtualDocument { get; set; }

        public MjmlDocument(MjmlParserOptions parserOptions)
        {
            _htmlParser = new HtmlParser();
            _parserOptions = parserOptions;
        }

        public void Parse(string mjml)
        {
            string preProcessed = ContentPreProcess(mjml);

            _document = _htmlParser.ParseDocument(preProcessed);

            if (_document.All.Any()) GenerateVirtualDocument();
        }

        public async Task ParseAsync(string mjml)
        {
            string preProcessed = ContentPreProcess(mjml);

            _document = await _htmlParser.ParseDocumentAsync(preProcessed);

            if (_document.All.Any()) GenerateVirtualDocument();
        }

        public string Compile()
        {
            // LR: Wrap the dynamically generated content in a the skeleton template
            string html = HtmlSkeleton.Build(VirtualDocument);

            // LR: Pass to the content post-processor
            string processed = ContentPostProcess(html);

            // LR: Respect parser options
            if (_parserOptions.Minify)
                return MinifyHtml(processed);
            else if (_parserOptions.Prettify)
                return PrettifyHtml(processed);

            // Faster
            return processed;
        }

        public async Task<string> CompileAsync()
        {
            // LR: Wrap the dynamically generated content in a the skeleton template
            string html = HtmlSkeleton.Build(VirtualDocument);

            // LR: Pass to the content post-processor
            string processed = ContentPostProcess(html);

            // LR: Respect parser options
            if (_parserOptions.Minify)
                return await MinifyHtmlAsync(processed);
            else if (_parserOptions.Prettify)
                return await PrettifyHtmlAsync(processed);

            // Faster
            return processed;
        }

        private string PrettifyHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            // LR: Parse the document using AngleSharp
            var document = _htmlParser.ParseDocument(html);

            return document.Prettify();
        }

        private async Task<string> PrettifyHtmlAsync(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            // LR: Parse the document using AngleSharp
            var document = await _htmlParser.ParseDocumentAsync(html);

            return document.Prettify();
        }

        private string MinifyHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            var document = _htmlParser.ParseDocument(html);

            return document.Minify();
        }

        private async Task<string> MinifyHtmlAsync(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            var document = await _htmlParser.ParseDocumentAsync(html);

            return document.Minify();
        }

        private string ContentPreProcess(string mjml)
        {
            // HACK: Unknown self-closing tags break AngleSharps DOM. Any siblings after unknown self-closing element becomes a child of the unkown element.
            // We can use regex to find and close the self-closing tags for AOT.
            Regex selfClosingMjml = new Regex(@"(<mj-.*\/>)", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline);

            mjml = selfClosingMjml.Replace(mjml, delegate (Match m)
            {
                string tagName = m.Value.Substring(1, m.Value.IndexOf(' ') - 1);
                return $"{m.Value.Replace("/>", "")}></{tagName}>";
            });

            return mjml;
        }

        private string ContentPostProcess(string html)
        {
            // LR: TODO Inline CSS

            return html;
        }

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

                case "mj-attributes":
                    return new MjmlAttributesComponent(element, parent, VirtualDocument);

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

                case "mj-divider":
                    return new MjmlDividerComponent(element, parent);

                case "mj-raw":
                    return new MjmlRawComponent(element, parent);

                case "mj-image":
                    return new MjmlImageComponent(element, parent);

                case "mj-button":
                    return new MjmlButtonComponent(element, parent);

                case "mj-hero":
                    return new MjmlHeroComponent(element, parent);

                case "mj-social":
                    return new MjmlSocialComponent(element, parent);

                case "mj-social-element":
                    return new MjmlSocialElementComponent(element, parent);

                case "mj-navbar":
                    return new MjmlNavbarComponent(element, parent);

                case "mj-navbar-link":
                    return new MjmlNavbarLinkComponent(element, parent);

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
            // Console.WriteLine($"Traversing <{element.NodeName.ToLowerInvariant()}>");

            if (!element.ChildNodes.Any())
                return;

            // LR: Traverse the children
            foreach (var childElement in element.ChildNodes)
            {
                BaseComponent childComponent;

                switch (childElement.NodeType)
                {
                    case NodeType.Element:
                        // LR: Create MJML component
                        childComponent = CreateMjmlComponent(childElement as Element, parentComponent);

                        // LR: Add child component to parent
                        parentComponent.Children.Add(childComponent);

                        // LR: Traverse the child element and change the parent context
                        TraverseElementTree(childElement, childComponent);
                        break;

                    case NodeType.Attribute:
                        break;

                    case NodeType.Text:
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
                        break;

                    case NodeType.CharacterData:
                        break;

                    case NodeType.EntityReference:
                        break;

                    case NodeType.Entity:
                        break;

                    case NodeType.ProcessingInstruction:
                        break;

                    case NodeType.Comment:
                        break;

                    case NodeType.Document:
                        break;

                    case NodeType.DocumentType:
                        break;

                    case NodeType.DocumentFragment:
                        break;

                    case NodeType.Notation:
                        break;

                    default:
                        break;
                }
            }
        }

        public void Dispose()
        {
            _document?.Dispose();
        }
    }
}