using System.Collections.Generic;

namespace MjmlDotNet.Components.Attributes
{
    internal static class GlobalDefaultAttributes
    {
        internal static Dictionary<string, string> NoAttributes = new Dictionary<string, string>();

        internal static class Head
        {
            internal static Dictionary<string, string> MjmlBreakpoint = new Dictionary<string, string>
            {
                { "width", null }
            };

            internal static Dictionary<string, string> MjmlFont = new Dictionary<string, string>
            {
                { "name", null },
                { "href", null }
            };

            internal static Dictionary<string, string> MjmlStyle = new Dictionary<string, string>
            {
                { "inline", null }
            };
        }

        internal static class Body
        {
            internal static Dictionary<string, string> MjmlBody = new Dictionary<string, string>
            {
                { "width", "600px" },
                { "background-color", string.Empty }
            };

            internal static Dictionary<string, string> MjmlButton = new Dictionary<string, string>
            {
               { "align", "center" },
                { "background-color", "#414141" },
                { "border-bottom", null },
                { "border-left", null },
                { "border-radius", "3px" },
                { "border-right", null },
                { "border-top", null },
                { "border", "none" },
                { "color", "#ffffff" },
                { "container-background-color", null },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-style", null },
                { "font-weight", "normal" },
                { "height", null },
                { "href", null },
                { "name", null },
                { "inner-padding", "10px 25px" },
                { "letter-spacing", null },
                { "line-height", "120%" },
                { "padding-bottom", null },
                { "padding-left", null },
                { "padding-right", null },
                { "padding-top", null },
                { "padding", "10px 25px" },
                { "rel", null },
                { "target", null },
                { "text-decoration", "none" },
                { "text-transform", "none" },
                { "vertical-align", "middle" },
                { "text-align", "center" },
                { "width", null },
            };

            internal static Dictionary<string, string> MjmlColumn = new Dictionary<string, string>
            {
                { "background-color", string.Empty },
                { "border", string.Empty },
                { "border-bottom", string.Empty },
                { "border-left", string.Empty },
                { "border-radius", string.Empty },
                { "border-right", string.Empty },
                { "border-top", string.Empty },
                { "direction", "ltr" },
                { "inner-background-color", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "padding-top", string.Empty },
                { "inner-border", string.Empty },
                { "inner-border-bottom", string.Empty },
                { "inner-border-left", string.Empty },
                { "inner-border-radius", string.Empty },
                { "inner-border-right", string.Empty },
                { "inner-border-top", string.Empty },
                { "padding", string.Empty },
                { "vertical-align", "top" },
                { "width", string.Empty },
                { "mobileWidth", null }
            };

            internal static Dictionary<string, string> MjmlDivider = new Dictionary<string, string>
            {
                { "border-color", "#000000" },
                { "border-style", "solid" },
                { "border-width", "4px" },
                { "container-background-color", string.Empty },
                { "css-class", string.Empty },
                { "padding", "10px 25px" },
                { "padding-top", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "width", "100%" },
            };

            internal static Dictionary<string, string> MjmlGroup = new Dictionary<string, string>
            {
                { "background-colour", null },
                { "direction", "ltr" },
                { "vertical-align", null },
                { "width", null }
            };

            internal static Dictionary<string, string> MjmlHero = new Dictionary<string, string>
            {
                { "background-color", "#FFFFFF" },
                { "background-height", null },
                { "background-position", "center center" },
                { "background-url", null },
                { "background-width", null },
                { "css-class", null },
                { "height", "0px" },
                { "mode", "fluid-height" },
                { "padding", "0px" },
                { "padding-bottom", null },
                { "padding-left", null },
                { "padding-right", null },
                { "padding-top", null },
                { "vertical-align", "top" },
                { "width", null },
            };

            internal static Dictionary<string, string> MjmlImage = new Dictionary<string, string>
            {
                { "alt", null },
                { "href", null },
                { "name", null },
                { "src", null },
                { "srcset", null },
                { "sizes", null },
                { "title", null },
                { "rel", null },
                { "align", "center" },
                { "border", "0" },
                { "border-bottom", null },
                { "border-left", null },
                { "border-right", null },
                { "border-top", null },
                { "border-radius", null },
                { "container-background-color", null },
                { "fluid-on-mobile", null },
                { "padding", "10px 25px" },
                { "padding-top", null },
                { "padding-bottom", null },
                { "padding-left", null },
                { "padding-right", null },
                { "target", "_blank" },
                { "width", null },
                { "height", "auto" },
                { "max-height", null },
                { "font-size", "13px" },
                { "usemap", null },
                { "vertical-align", null },
                { "css-class", null }
            };

            internal static Dictionary<string, string> MjmlNavbar = new Dictionary<string, string>
            {
                { "align", "center" },
                { "base-url", null },
                { "css-class", null },
                { "hamburger", null },
                { "ico-align", "center" },
                { "ico-open", "&#9776;" },
                { "ico-close", "&#8855;" },
                { "ico-color", "#000000" },
                { "ico-font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "ico-font-size", "30px" },
                { "ico-line-height", "30px" },
                { "ico-padding", "10px" },
                { "ico-padding-bottom", "10px" },
                { "ico-padding-left", "10px" },
                { "ico-padding-right", "10px" },
                { "ico-padding-top", "10px" },
                { "ico-text-decoration", null },
                { "ico-text-transform", null }
            };

            internal static Dictionary<string, string> MjmlNavbarLink = new Dictionary<string, string>
            {
                { "color", "#000000" },
                { "css-class", null },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-weight", null },
                { "href", null },
                { "letter-spacing", null },
                { "line-height", "22px" },
                { "padding", "15px 10px" },
                { "padding-bottom", null },
                { "padding-left", null },
                { "padding-right", null },
                { "padding-top", null },
                { "rel", null },
                { "target", null },
                { "text-decoration", null },
                { "text-transform", "uppercase" }
            };

            internal static Dictionary<string, string> MjmlSection = new Dictionary<string, string> {
                { "background-color", null },
                { "background-repeat", "repeat" },
                { "background-size", "auto" },
                { "background-url", null },
                { "border", string.Empty },
                { "border-bottom", string.Empty },
                { "border-left", string.Empty },
                { "border-radius", string.Empty },
                { "border-right", string.Empty },
                { "border-top", string.Empty },
                { "css-class", string.Empty },
                { "direction", "ltr" },
                { "full-width", string.Empty },
                { "padding", "20px 0" },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "padding-top", string.Empty },
                { "text-align", "center" },
                { "text-padding", "4px 4px 4px 0" },

                { "background-position", "top center" },
                { "background-position-x", null },
                { "background-position-y", null },
            };

            internal static Dictionary<string, string> MjmlSocial = new Dictionary<string, string>
            {
                { "align", "center" },
                { "border-radius", "3px" },
                { "color", "#333333" },
                { "css-class", string.Empty },
                { "container-background-color", string.Empty },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-style", "normal" },
                { "font-weight", "normal" },
                { "icon-height", "icon-size" },
                { "icon-size", "20px" },
                { "inner-padding", "4px" },
                { "line-height", "22px" },
                { "mode", "horizontal" },
                { "padding", "10px 25px" },
                { "padding-top", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "icon-padding", "0px" },
                { "text-padding", "4px 4px 4px 0" },
                { "text-decoration", string.Empty }
            };

            internal static Dictionary<string, string> MjmlSocialElement = new Dictionary<string, string>
            {
                { "align", "center" },
                { "alt", string.Empty },
                { "background-color", string.Empty },
                { "border-radius", "3px" },
                { "color", "#333333" },
                { "css-color", string.Empty },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-style", "normal" },
                { "font-weight", "normal" },
                { "href", string.Empty },
                { "icon-height", "icon-size" },
                { "icon-size", "20px" },
                { "line-height", "22px" },
                { "name", string.Empty },
                { "padding", "4px" },
                { "padding-top", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "icon-padding", "0px" },
                { "text-padding", "4px 4px 4px 0" },
                { "sizes", string.Empty },
                { "src", string.Empty },
                { "srcset", string.Empty },
                { "target", "_blank" },
                { "title", string.Empty },
                { "text-decoration", string.Empty },
                { "vertical-align", "middle" },
            };

            internal static Dictionary<string, string> MjmlSpacer = new Dictionary<string, string>
            {
                { "border", null },
                { "border-bottom", null },
                { "border-left", null },
                { "border-right", null },
                { "border-top", null },
                { "container-background-color", null },
                { "padding-bottom", null },
                { "padding-left", null },
                { "padding-right", null },
                { "padding-top", null },
                { "padding", null },
                { "vertical-align", "middle" },
                { "width", null },
                { "height", "20px" },
            };

            internal static Dictionary<string, string> MjmlText = new Dictionary<string, string>
            {
                { "color", "#000000" },
                { "font-family", "Ubuntu, Helvetica, Arial, sans-serif" },
                { "font-size", "13px" },
                { "font-style", string.Empty },
                { "font-weight", string.Empty },
                { "line-height", "1" },
                { "letter-spacing", "none" },
                { "height", string.Empty },
                { "text-decoration", string.Empty },
                { "text-transform", string.Empty },
                { "align", "left" },
                { "container-background-color", string.Empty },
                { "padding", "10px 25px" },
                { "padding-top", string.Empty },
                { "padding-bottom", string.Empty },
                { "padding-left", string.Empty },
                { "padding-right", string.Empty },
                { "css-class", string.Empty }
            };
        }
    }
}