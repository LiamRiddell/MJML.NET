using System.Threading.Tasks;

namespace MjmlDotNet.Core.Interfaces
{
    internal interface IMjmlDocument
    {
        /// <summary>
        /// Compiles the MJML document to native HTML.
        /// </summary>
        string Compile();

        /// <summary>
        /// Compiles the MJML document to native HTML.
        /// </summary>
        Task<string> CompileAsync();

        /// <summary>
        /// Parses the MJML.
        /// </summary>
        void Parse(string mjml);

        /// <summary>
        /// Parses the MJML.
        /// </summary>
        Task ParseAsync(string mjml);
    }
}