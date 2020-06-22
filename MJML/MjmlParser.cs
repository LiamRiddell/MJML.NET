using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MJML
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