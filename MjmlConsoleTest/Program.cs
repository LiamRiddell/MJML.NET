using System;
using System.Diagnostics;
using System.IO;
using Mjml;

namespace MjmlConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string mjmlString = @"
                <mjml>
                  <mj-body>
                    <mj-wrapper border=""1px solid #000000"" padding=""20px"">
                      <mj-section padding=""20px"" background-url=""https://via.placeholder.com/600x167"" background-repeat=""no-repeat"">
                        <mj-column border=""1px solid #dddddd"">
                          <mj-text padding=""20px""> First line of text </mj-text>
                        </mj-column>
                        <mj-column border=""1px solid #dddddd"">
                          <mj-text padding=""20px""> First line of text </mj-text>
                        </mj-column>
                      </mj-section>
                      <mj-section background-color=""#ffffff"" padding-left=""0"" padding-right=""0"" padding-top=""0"">
                        <mj-column width=""50%"">
                          <mj-image align=""center"" css-class=""LR-CLASS-TEST"" vertical-align=""middle"" src=""https://res.cloudinary.com/dheck1ubc/image/upload/v1544153577/Email/Images/AnnouncementOffset/Image_1.png"" alt="""" />
                        </mj-column>
                        <mj-column width=""50%"">
                          <mj-image align=""center"" src=""https://res.cloudinary.com/dheck1ubc/image/upload/v1544153578/Email/Images/AnnouncementOffset/Image_2.png"" alt="""" />
                        </mj-column>
                      </mj-section>
                    </mj-wrapper>

                    <mj-section background-color=""#ffffff"" padding-left=""0"" padding-right=""0"" padding-top=""0"">
                        <mj-column>
                            <mj-button font-family=""Helvetica"" background-color=""#f45e43"" color=""white"" href=""https://www.google.co.uk"">
                                Don't click me!
                            </mj-button>
                        </mj-column>
                    </mj-section>
                  </mj-body>
                </mjml>
            ";

            var sw = Stopwatch.StartNew();
            for (int i = 0; i <= 0; i++)
            {
                var mjmlParser = new MjmlParser();

                if (!mjmlParser.TryParse(mjmlString, new { }, out var html))
                {
                    // ... handle failure
                }

                try
                {
                    html = System.Xml.Linq.XElement.Parse(html).ToString();
                }
                catch (Exception) { }

                Console.WriteLine(html);
                File.WriteAllText("./index.html", html);
            }
            sw.Stop();

            Console.WriteLine($"Parsed: {sw.Elapsed}");
        }
    }
}