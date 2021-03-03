﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MjmlDotNet.Core.Css
{
    internal class CssStyleLibraries
    {
        private Dictionary<string, Dictionary<string, string>> Libraries { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public void AddStyleLibrary(string libraryName, Dictionary<string, string> libraryStyles)
        {
            this.Libraries.Add(libraryName, libraryStyles);
        }

        public Dictionary<string, string> GetStyleLibrary(string libraryName)
        {
            if (this.Libraries.ContainsKey(libraryName))
            {
                return this.Libraries[libraryName];
            }

            this.AddStyleLibrary(libraryName, new Dictionary<string, string>() { });

            return GetStyleLibrary(libraryName);
        }

        public void Clear()
        {
            Libraries.Clear();
        }

        public bool Any()
        {
            return Libraries.Any();
        }
    }
}