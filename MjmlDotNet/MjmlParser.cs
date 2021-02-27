using MjmlDotNet.Core.Document;

namespace MjmlDotNet
{
    public class MjmlParser : IMjmlParser
    {
        public MjmlParser()
        {
        }

        public string ParseDocument(string mjml)
        {
            MjmlDocument mjmlDocument = new MjmlDocument(mjml);
            return mjmlDocument.Compile(true);
        }

        public bool TryParseDocument(string content, out string html)
        {
            html = string.Empty;

            try
            {
                html = ParseDocument(content);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}