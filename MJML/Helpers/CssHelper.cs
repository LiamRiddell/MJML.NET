using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mjml.Helpers
{
    static class CssHelper
    {
        public static string SuffixCssClasses(string cssClasses, string suffix)
        {
            if (string.IsNullOrWhiteSpace(cssClasses) || string.IsNullOrWhiteSpace(suffix))
                return string.Empty;

            var classes = cssClasses.Split(' ');

            if (!classes.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var klass in classes)
            {
                sb.Append($" {klass}-{suffix}");
            }

            return sb.ToString().Trim();
        }
    }
}
