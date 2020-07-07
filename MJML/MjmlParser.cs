namespace Mjml
{
    public class MjmlParser
    {
        public MjmlParser()
        {
        }

        public bool TryParse(string content, object options, out string html)
        {
            MjmlDocument mjmlDocument = new MjmlDocument(content);
            mjmlDocument.Parse();

            html = mjmlDocument.Render();

            return true;
        }
    }
}