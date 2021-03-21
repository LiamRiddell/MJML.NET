using MjmlDotNet.Core.Document;
using System.Threading.Tasks;

namespace MjmlDotNet
{
    public class MjmlParser : IMjmlParser
    {
        private readonly MjmlParserOptions _defaultOptions;

        public MjmlParser()
        {
            _defaultOptions = new MjmlParserOptions()
            {
                Minify = true,
                Prettify = false
            };
        }

        public MjmlParser(MjmlParserOptions defaultOptions)
        {
            _defaultOptions = defaultOptions;
        }

        public string ParseDocument(string mjml)
        {
            using (MjmlDocument mjmlDocument = new MjmlDocument(_defaultOptions))
            {
                mjmlDocument.Parse(mjml);
                return mjmlDocument.Compile();
            }
        }

        public string ParseDocument(string mjml, MjmlParserOptions parserOptions)
        {
            using (MjmlDocument mjmlDocument = new MjmlDocument(parserOptions))
            {
                mjmlDocument.Parse(mjml);
                return mjmlDocument.Compile();
            }
        }

        public bool TryParseDocument(string mjml, out string html)
        {
            html = string.Empty;

            try
            {
                html = ParseDocument(mjml);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool TryParseDocument(string mjml, MjmlParserOptions parserOptions, out string html)
        {
            html = string.Empty;

            try
            {
                html = ParseDocument(mjml, parserOptions);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<string> ParseDocumentAsync(string mjml)
        {
            using (MjmlDocument mjmlDocument = new MjmlDocument(_defaultOptions))
            {
                await mjmlDocument.ParseAsync(mjml);
                return await mjmlDocument.CompileAsync();
            }
        }

        public async Task<string> ParseDocumentAsync(string mjml, MjmlParserOptions parserOptions)
        {
            using (MjmlDocument mjmlDocument = new MjmlDocument(parserOptions))
            {
                await mjmlDocument.ParseAsync(mjml);
                return await mjmlDocument.CompileAsync();
            }
        }
    }
}