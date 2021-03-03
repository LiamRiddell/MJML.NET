using System.Collections.Generic;
using System.Linq;

namespace MjmlDotNet.Core.Css
{
    internal class CssStyleLibraries
    {
        private Dictionary<string, Dictionary<string, string>> Libraries { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public void AddStyleLibrary(string libraryName, Dictionary<string, string> libraryStyles)
        {
            if (Libraries.ContainsKey(libraryName))
            {
                Libraries[libraryName] = libraryStyles;
                return;
            }

            Libraries.Add(libraryName, libraryStyles); 
        }

        public Dictionary<string, string> GetStyleLibrary(string libraryName)
        {
            if (Libraries.ContainsKey(libraryName))
            {
                return Libraries[libraryName];
            }

            AddStyleLibrary(libraryName, new Dictionary<string, string>() { });

            return GetStyleLibrary(libraryName);
        }

        public bool Any()
        {
            return Libraries.Any();
        }
    }
}