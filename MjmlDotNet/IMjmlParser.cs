using System.Threading.Tasks;

namespace MjmlDotNet
{
    public interface IMjmlParser
    {
        /// <summary>
        /// Parses the MJML document.
        /// </summary>
        string ParseDocument(string mjml);

        /// <summary>
        /// Parses the MJML document.
        /// </summary>
        string ParseDocument(string mjml, MjmlParserOptions parserOptions);

        /// <summary>
        /// Try parses the MJML document.
        /// </summary>
        bool TryParseDocument(string mjml, out string html);

        /// <summary>
        /// Try parses the MJML document.
        /// </summary>
        bool TryParseDocument(string mjml, MjmlParserOptions parserOptions, out string html);

        /// <summary>
        /// Parses the MJML document.
        /// </summary>
        Task<string> ParseDocumentAsync(string mjml);

        /// <summary>
        /// Parses the MJML document.
        /// </summary>
        Task<string> ParseDocumentAsync(string mjml, MjmlParserOptions parserOptions);
    }
}