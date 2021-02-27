using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MjmlDotNet;

namespace MjmlDotNet.Tests.Components.Mjml.Head
{
    [TestClass]
    public class MjmlTitleComponent
    {
        private IHtmlParser _HtmlParser { get; set; }
        private IDocument _HtmlDocument { get; set; }
        private MjmlParser _MjmlParser { get; set; }

        public MjmlTitleComponent()
        {
            _MjmlParser = new MjmlParser();
            _HtmlParser = new HtmlParser();
        }

        [TestMethod]
        public void ValidateDocumentTitle()
        {
            string documentTitle = "Hello MjmlDotNet";

            string mjml = $@"
                <mjml>
                  <mj-head>
                    <mj-title>{documentTitle}</mj-title>
                  </mj-head>
                  <mj-body>
                  </mj-body>
                </mjml>
            ";

            // LR: Compile to HTML
            var html = _MjmlParser.Parse(mjml, new object { });

            // LR: Feed the output into AngleSharp
            _HtmlDocument = _HtmlParser.ParseDocument(html);

            // LR: Validate the document title
            Assert.AreEqual(documentTitle, _HtmlDocument.Title);
        }
    }
}
