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
                    <mj-section>
                      <mj-group>
                        <mj-column>
                          <br>
                          <mj-image width=""137px"" height=""185px"" padding=""0"" src=""https://mjml.io/assets/img/easy-and-quick.png"" />
                          <mj-text align=""center"">
                            <h2>Easy and quick</h2>
                            <p>Write less code, save time and code more efficiently with MJML’s semantic syntax.</p>
                          </mj-text>
                        </mj-column>
                        <mj-column>
                          <mj-image width=""166px"" height=""185px"" padding=""0"" src=""https://mjml.io/assets/img/responsive.png"" />
                          <mj-text align=""center"">
                            <h2>Responsive</h2>
                            <p>MJML is responsive by design on most-popular email clients, even Outlook.</p>
                          </mj-text>
                        </mj-column>
                      </mj-group>
                      <mj-column>
                        <mj-image width=""166px"" height=""185px"" padding=""0"" src=""https://mjml.io/assets/img/responsive.png"" />
                        <mj-text align=""center"">
                          <h2>Responsive</h2>
                          <p>MJML is responsive by design on most-popular email clients, even Outlook.</p>
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
                    html = html.ToString();
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