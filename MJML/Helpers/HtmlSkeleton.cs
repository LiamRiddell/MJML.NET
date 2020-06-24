using System;
using System.Collections.Generic;
using System.Text;

namespace Mjml.Helpers
{
    // https://github.com/mjmlio/mjml/blob/604a9f2d2c19a224590814615fda8666fe1fd4e4/packages/mjml-core/src/helpers/skeleton.js
    public class HtmlSkeleton
    {
        public string BackgroundColor { get; set; }
        public string Breakpoint { get; set; }
        public Dictionary<string, string> Fonts { get; set; }
        public Dictionary<string, string> MediaQueries { get; set; }
        // public Dictionary<string, string> MediaQueries { get; set; }

        public HtmlSkeleton()
        {
        }

        public string Build(string content)
        {
            return $@"
            <!doctype html>
            <html TODO:{{langAttribute}} xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v="" urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
            <head>
                <title>
                    TODO:{{title}}
                </title>
                <!--[if !mso]><!-- -->
                <meta http-equiv="" X-UA-Compatible"" content="" IE=edge"">
                <!--<![endif]-->
                <meta http-equiv="" Content-Type"" content="" text/html; charset=UTF-8"">
                <meta name="" viewport"" content="" width=device-width, initial-scale=1"">
                <style type="" text/css"">
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

                <style type="" text/css"">
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
                style && style.length > 0
                ? `<style type="" text/css"">{{style.join('')}}</style>`
                : ''
                }}
                TODO:{{headRaw.filter(negate(isNil)).join('\n')}}
            </head>
            <body TODO:{{ backgroundColor === '' ? '' : ` style="" background-color:{{backgroundColor}};""` }}>
                TODO:{{buildPreview(preview)}}
                {content}
                </body>
            </html>
            ";
        }
    }
}