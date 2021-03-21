using System;
using System.Linq;

namespace MjmlDotNet.Core.Helpers
{
    internal static class TagHelpers
    {
        private const string startConditionalTag = "<!--[if mso | IE]>";
        private const string startMsoConditionalTag = "<!--[if mso]>";
        private const string endConditionalTag = "<![endif]-->";
        private const string startNegationConditionalTag = "<!--[if !mso | IE]><!-->";
        private const string startMsoNegationConditionalTag = "<!--[if !mso><!-->";
        private const string endNegationConditionalTag = "<!--<![endif]-->";

        private readonly static Random _random = new Random();

        public static string ConditionalTag(string content, bool negation = false)
        {
            return $@"
                {(negation ? startNegationConditionalTag : startConditionalTag)}
                {content}
                {(negation ? endNegationConditionalTag : endConditionalTag)}
            ";
        }

        public static string MsoConditionalTag(string content, bool negation = false)
        {
            return $@"
                {(negation ? startMsoNegationConditionalTag : startMsoConditionalTag)}
                {content}
                {(negation ? endNegationConditionalTag : endConditionalTag)}
            ";
        }

        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            _random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + _random.Next(16).ToString("X");
        }
    }
}