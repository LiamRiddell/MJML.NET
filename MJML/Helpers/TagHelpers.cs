namespace Mjml.Helpers
{
    public static class TagHelpers
    {
        private const string startConditionalTag = "<!--[if mso | IE]>";
        private const string startMsoConditionalTag = "<!--[if mso]>";
        private const string endConditionalTag = "<![endif]-->";
        private const string startNegationConditionalTag = "<!--[if !mso | IE]><!-->";
        private const string startMsoNegationConditionalTag = "<!--[if !mso><!-->";
        private const string endNegationConditionalTag = "<!--<![endif]-->";

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
    }
}