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
                        <mj-section>
                            <mj-column>
                                <mj-text>
                                    Hello World!
                                </mj-text>
                            </mj-column>
                            <mj-column>
                                <mj-text>
                                    Hello World!
                                </mj-text>
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
                File.WriteAllText("./index-fixed.html", html);
            }
            sw.Stop();

            Console.WriteLine($"Parsed: {sw.Elapsed}");
        }
    }
}