using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mjml.Helpers
{
    // https://github.com/mjmlio/mjml/blob/604a9f2d2c19a224590814615fda8666fe1fd4e4/packages/mjml-core/src/helpers/skeleton.js
    public static class HtmlSkeleton
    {
        public static string Title { get; set; } = "MJML.NET";
        public static string PreviewText { get; set; } = "Example MJML.NET";
        public static string Language { get; set; } = string.Empty;
        public static string ContainerWidth { get; set; } = "600px";
        public static string BackgroundColor { get; set; } = "white";
        public static string Breakpoint { get; set; } = "320px";

        public static Dictionary<string, string> Fonts { get; set; } = new Dictionary<string, string>() {
            { "Open Sans", "https://fonts.googleapis.com/css?family=Open+Sans:300,400,500,700" },
            { "Droid Sans", "https://fonts.googleapis.com/css?family=Droid+Sans:300,400,500,700" },
            { "Lato", "https://fonts.googleapis.com/css?family=Lato:300,400,500,700" },
            { "Roboto", "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" },
            { "Ubuntu", "https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700" }
        };

        public static Dictionary<string, string> MediaQueries { get; set; } = new Dictionary<string, string>();
        public static Dictionary<string, string> InlineStyle { get; set; } = new Dictionary<string, string>();

        // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/helpers/fonts.js
        public static string BuildFontsTags(string content, string inlineStyle)
        {
            List<string> fontsToImport = new List<string>();

            foreach (var font in Fonts)
            {
                Regex regex = new Regex($@"""[^""]*font-family:[^""]*{font.Key}[^""]*""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Regex inlineRegex = new Regex($@"font-family:[^;}}]*${font.Key}", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/helpers/fonts.js#L11
                if (regex.IsMatch(content))
                {
                    fontsToImport.Add(font.Value);
                }
            }

            if (!fontsToImport.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"<!--[if !mso]><!-->");
            // LR <link>
            foreach (var fontUrl in fontsToImport)
            {
                sb.AppendLine($@"<link href=""{fontUrl}"" rel=""stylesheet"" type=""text/css"">");
            }

            // LR: <style>
            sb.AppendLine($@"<style type=""text/css"">");
            foreach (var fontUrl in fontsToImport)
            {
                sb.AppendLine($@"@import url(""{fontUrl}"");");
            }
            sb.AppendLine($@"</style>");
            sb.AppendLine($@"<!--<![endif]-->");

            return sb.ToString();
        }

        // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-core/src/helpers/mediaQueries.js
        private static string BuildMediaQueriesTags(bool forceOWADesktop = false)
        {
            if (!MediaQueries.Any())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append($@"
                <style type=""text/css"">
                    @media only screen and (min-width:{Breakpoint}) {{
            ");

            foreach (var mediaQuery in MediaQueries)
            {
                sb.AppendLine($@".{mediaQuery.Key} {mediaQuery.Value}");
            }

            sb.Append($@"
                    }}
                </style>
            ");

            if (forceOWADesktop)
            {
                sb.AppendLine($@"<style type=""text/css"">");

                foreach (var mediaQuery in MediaQueries)
                {
                    sb.AppendLine($@"[owa] .{mediaQuery.Key} {mediaQuery.Value}");
                }

                sb.AppendLine($@"</style>");
            }

            return sb.ToString();
        }

        private static string BuildPreview()
        {
            if (string.IsNullOrWhiteSpace(PreviewText))
                return string.Empty;

            return $@"
                <div style=""display:none;font-size:1px;color:#ffffff;line-height:1px;max-height:0px;max-width:0px;opacity:0;overflow:hidden;"">
                    {PreviewText}
                </div>
            ";
        }

        public static string Build(string content)
        {
            bool forceOWADesktop = false;

            return $@"
            <!doctype html>
            <html {(!string.IsNullOrWhiteSpace(Language) ? $@"lang=""{Language}"" " : string.Empty)}xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
                <head>
                    <title>
                        {Title}
                    </title>
                    <!--[if !mso]><!-- -->
                        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                    <!--<![endif]-->
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                    <style type=""text/css"">
                        #outlook a {{ padding:0; }}
                        body {{ margin:0;padding:0;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%; }}
                        table, td {{ border-collapse:collapse;mso-table-lspace:0pt;mso-table-rspace:0pt; }}
                        img {{ border:0;height:auto;line-height:100%; outline:none;text-decoration:none;-ms-interpolation-mode:bicubic; }}
                        p {{ display:block;margin:13px 0; }}
                    </style>
                    <!--[if mso]>
                        <xml>
                            <o:OfficeDocumentSettings>
                              <o:AllowPNG/>
                              <o:PixelsPerInch>96</o:PixelsPerInch>
                            </o:OfficeDocumentSettings>
                        </xml>
                    <![endif]-->
                    <!--[if lte mso 11]>
                        <style type=""text/css"">
                          .mj-outlook-group-fix {{ width:100% !important; }}
                        </style>
                    <![endif]-->

                    { BuildFontsTags(content, "") /* TODO: Inline Support */ }
                    { BuildMediaQueriesTags(forceOWADesktop) }

                   <!--
                    <style type=""text/css"">
                        TODO:{{reduce(
                          componentsHeadStyle,
                          (result, compHeadStyle) => `{{result}}\n{{compHeadStyle(breakpoint)}}`,
                          '',
                        )}}

                        TODO:{{reduce(
                          headStyle,
                          (result, headStyle) => `{{result}}\n{{headStyle(breakpoint)}}`,
                          '',
                        )}}
                    </style>

                    TODO:{{
                        style && style.length > 0 ? `<style type="" text/css"">{{style.join('')}}</style>` : ''
                    }}

                    TODO:{{ headRaw.filter(negate(isNil)).join('\n') }}
                    -->
                </head>
                <body{(string.IsNullOrWhiteSpace(BackgroundColor) ? string.Empty : $@" style=""background-color:{BackgroundColor};""") }>
                    { BuildPreview() }
                    { content }
                </body>
            </html>
            ";
        }

        public static void AddMediaQuery(string className, CssParsedUnit cssParsedUnit)
        {
            string mediaQuery = $"{{ width:{cssParsedUnit} !important; max-width: {cssParsedUnit}; }}";

            if (MediaQueries.ContainsKey(className))
            {
                var mediaQueryCurrent = MediaQueries[className];

                if (mediaQueryCurrent.Equals(mediaQuery, StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            MediaQueries.Add(className, mediaQuery);
        }

        public static void AddFont(string name, string href)
        {
            if (Fonts.ContainsKey(name))
            {
                var hrefCurrent = MediaQueries[name];

                if (hrefCurrent.Equals(href, StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            Fonts.Add(name, href);
        }
    }
}