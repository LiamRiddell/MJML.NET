﻿using System;
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
                        <mj-breakpoint width=""320px"" />
                    </mj-head>
                    <mj-body>
                        <mj-section background-color=""blue"">
                            <mj-column>
                                <mj-text font-size="" 20px"" color="" #F45E43"" font-family="" helvetica"">Hello World</mj-text>
                                <mj-divider border-color="" #F45E43""></mj-divider>
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

                string formattedHtml = System.Xml.Linq.XElement.Parse(html).ToString();

                Console.WriteLine(formattedHtml);
            }
            sw.Stop();

            Console.WriteLine($"Parsed: {sw.Elapsed}");
        }
    }
}