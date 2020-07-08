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
                            <mj-column inner-border=""10px"">
                                <mj-text>
                                    Column 1
                                </mj-text>
                                <mj-spacer height=""50px"" />
                                <mj-text>
                                    Affected by 50px spacer
                                </mj-text>
                            </mj-column>
                            <mj-column>
                                <mj-text>
                                    Column 2
                                </mj-text>

                                <mj-raw>
                                    {{ if counter % 5 }}
                                </mj-raw>

                                <mj-raw>
                                    <h1>Test <span>Test</span></h1>
                                    <mj-text>Ignored</mj-text>
                                </mj-raw>

                                <mj-raw>
                                    {{ end if}}
                                </mj-raw>
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