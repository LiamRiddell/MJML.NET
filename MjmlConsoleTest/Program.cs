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
                    <mj-head>
                        <mj-title>MJML.NET</mj-title>
                        <mj-preview>Liam Riddell</mj-preview>
                    </mj-head>
                    <mj-body>
                        <mj-wrapper border=""1px solid #000000"" padding=""50px 30px"">
                          <mj-section border-top=""1px solid #aaaaaa"" border-left=""1px solid #aaaaaa"" border-right=""1px solid #aaaaaa"" padding=""20px"">
                            <mj-column>

                            </mj-column>
                          </mj-section>
                          <mj-section border-left=""1px solid #aaaaaa"" border-right=""1px solid #aaaaaa"" padding=""20px"" border-bottom=""1px solid #aaaaaa"">
                            <mj-column border=""1px solid #dddddd"">
                              <mj-text padding=""20px""> First line of text </mj-text>
                              <mj-divider border-width=""1px"" border-style=""dashed"" border-color=""lightgrey"" padding=""0 20px"" />
                              <mj-text padding=""20px""> Second line of text </mj-text>
                            </mj-column>
                          </mj-section>
                        </mj-wrapper>
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