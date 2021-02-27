using System;
using System.Collections.Generic;
using System.Text;

namespace MjmlDotNet
{
    public interface IMjmlParser
    {
        /// <summary>
        /// Parses the MJML document.
        /// </summary>
        string ParseDocument(string mjml);

        /// <summary>
        /// Tries to parse the MJML document.
        /// </summary>
        bool TryParseDocument(string mjml, out string html);
    }
}
