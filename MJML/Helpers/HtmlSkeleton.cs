using System;
using System.Collections.Generic;
using System.Text;

namespace Mjml.Helpers
{
    // https://github.com/mjmlio/mjml/blob/604a9f2d2c19a224590814615fda8666fe1fd4e4/packages/mjml-core/src/helpers/skeleton.js
    public static class HtmlSkeleton
    {
        public static string Title { get; set; } = "MJML.NET";
        public static string Language { get; set; } = string.Empty;

        public static string BackgroundColor { get; set; }
        public static string Breakpoint { get; set; } = "480px";

        public static Dictionary<string, string> Fonts { get; set; } = new Dictionary<string, string>() {
            { "Open Sans", "https://fonts.googleapis.com/css?family=Open+Sans:300,400,500,700" },
            { "Droid Sans", "https://fonts.googleapis.com/css?family=Droid+Sans:300,400,500,700" },
            { "Lato", "https://fonts.googleapis.com/css?family=Lato:300,400,500,700" },
            { "Roboto", "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" },
            { "Ubuntu", "https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700" }
        };

        public static Dictionary<string, string> MediaQueries { get; set; }

        public static string Build(string content)
        {
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
                        table, td {{ border - collapse:collapse;mso-table-lspace:0pt;mso-table-rspace:0pt; }}
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

                    TODO:{{buildFontsTags(content, inlineStyle, fonts)}}
                    TODO:{{buildMediaQueriesTags(breakpoint, mediaQueries, forceOWADesktop)}}

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
                </head>
                <body{(string.IsNullOrWhiteSpace(BackgroundColor) ? string.Empty : $@" style=""background-color:{BackgroundColor};""") }>
                    TODO:{{buildPreview(preview)}}
                    {content}
                </body>
            </html>
            ";
        }
    }
}