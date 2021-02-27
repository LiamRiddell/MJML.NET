using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MjmlDotNet.Tests.Components.Mjml.Head
{
    [TestClass]
    public class MjmlTitleComponent
    {
        private IHtmlParser _HtmlParser { get; set; }
        private IDocument _HtmlDocument { get; set; }
        private IMjmlParser _MjmlParser { get; set; }

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

            var html = _MjmlParser.ParseDocument(mjml);

            _HtmlDocument = _HtmlParser.ParseDocument(html);

            Assert.AreEqual(documentTitle, _HtmlDocument.Title);
        }
    }
}
