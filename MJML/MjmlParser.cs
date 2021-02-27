using System.Threading.Tasks;

namespace MjmlDotNet
{
    public class MjmlParser
    {
        public MjmlParser()
        {
        }

        public string Parse(string content, object options)
        {
            // LR: Intialise MjmlDocument with the content
            MjmlDocument mjmlDocument = new MjmlDocument(content);

            // LR: Parse the Mjml
            mjmlDocument.Parse();

            // LR: Render the MJML to HTML
            return mjmlDocument.Render(true);
        }

        public bool TryParse(string content, object options, out string html)
        {
            // LR: Default output to an empty string
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