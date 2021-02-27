namespace MjmlDotNet
{
    public class MjmlParser : IMjmlParser
    {
        public MjmlParser()
        {
        }

        public string Parse(string content, object options)
        {
            MjmlDocument mjmlDocument = new MjmlDocument(content);

            mjmlDocument.Parse();

            return mjmlDocument.Render(true);
        }

        public bool TryParse(string content, object options, out string html)
        {
            html = string.Empty;

            try
            {
                html = Parse(content, options);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}