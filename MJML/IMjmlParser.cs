using System;
using System.Collections.Generic;
using System.Text;

namespace MjmlDotNet
{
    public interface IMjmlParser
    {
        /// <summary>
        /// Parses the MJML content using the specified options.
        /// </summary>
        /// <param name="content">MJML Content</param>
        /// <param name="options"></param>
        /// <returns>A string containing the compiled HTML.</returns>
        string Parse(string content, object options);

        /// <summary>
        /// Tries to parse the MJML content using the specified options.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="options"></param>
        /// <param name="html">A string containing the compiled HTML</param>
        /// <returns>A boolean value based upon operation success.</returns>
        bool TryParse(string content, object options, out string html);
    }
}
