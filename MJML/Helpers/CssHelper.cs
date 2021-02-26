using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MjmlDotNet.Helpers
{
    internal static class CssHelper
    {
        private readonly static List<string> CssProperties = new List<string>
        {
            "background",
            "background-attachment",
            "background-clip",
            "background-color",
            "background-image",
            "background-origin",
            "background-position",
            "background-position-x",
            "background-position-y",
            "background-repeat",
            "background-size",
            "border",
            "border-collapse",
            "border-color",
            "border-spacing",
            "border-style",
            "border-top",
            "border-right",
            "border-bottom",
            "border-left",
            "border-top-color",
            "border-right-color",
            "border-bottom-color",
            "border-left-color",
            "border-top-style",
            "border-right-style",
            "border-bottom-style",
            "border-left-style",
            "border-top-width",
            "border-right-width",
            "border-bottom-width",
            "border-left-width",
            "border-width",
            "bottom",
            "caption-side",
            "clear",
            "clip",
            "color",
            "content",
            "counter-increment",
            "counter-reset",
            "cursor",
            "direction",
            "display",
            "empty-cells",
            "float",
            "font",
            "font-family",
            "font-size",
            "font-size-adjust",
            "font-style",
            "font-variant",
            "font-weight",
            "height",
            "left",
            "letter-spacing",
            "line-height",
            "list-style",
            "list-style-image",
            "list-style-position",
            "list-style-type",
            "margin",
            "margin-top",
            "margin-right",
            "margin-bottom",
            "margin-left",
            "max-height",
            "max-width",
            "min-height",
            "min-width",
            "outline",
            "outline-color",
            "outline-style",
            "outline-width",
            "overflow",
            "padding",
            "padding-top",
            "padding-right",
            "padding-bottom",
            "padding-left",
            "page-break-after",
            "page-break-before",
            "page-break-inside",
            "position",
            "quotes",
            "right",
            "table-layout",
            "text-align",
            "text-decoration",
            "text-indent",
            "text-shadow",
            "text-transform",
            "top",
            "vertical-align",
            "visibility",
            "white-space",
            "width",
            "word-spacing",
            "z-index"
        };

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

        public static bool IsCssProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return false;

            return CssProperties.Contains(propertyName.ToLower());
        }
    }
}