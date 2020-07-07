using System;
using System.Diagnostics;
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
                        <mj-title>Custom Title</mj-title>
                        <mj-preview>Liam Riddell</mj-preview>
                        <mj-breakpoint width=""480px"" />
                        <mj-font name=""Raleway"" href=""https://fonts.googleapis.com/css?family=Raleway"" />
                    </mj-head>
                    <mj-body background-color=""red"">
                        <mj-section>
                            <mj-column padding-right=""100px"">
                                <mj-text>
                                    Hello World!
                                </mj-text>
                            </mj-column>
                            <mj-column>
                                <mj-text font-family=""Raleway, Ubuntu, Helvetica, Arial, sans-serif"">
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
            }
            sw.Stop();

            Console.WriteLine($"Parsed: {sw.Elapsed}");
        }
    }
}