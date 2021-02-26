using AngleSharp.Dom;
using MjmlDotNet.Core.Component;
using System.Collections.Generic;
using System.Linq;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-text/src/index.js
    public class MjmlSocialElementComponent : BodyComponent
    {
        private class SocialNetworkSettings
        {
            public string ShareUrl { get; set; }
            public string BackgroundColor { get; set; }
            public string Src { get; set; }

            public SocialNetworkSettings(string shareUrl, string backgroundColor, string src)
            {
                ShareUrl = shareUrl;
                BackgroundColor = backgroundColor;
                Src = src;
            }
        }

        private const string IMAGE_BASE_URL = "https://www.mailjet.com/images/theme/v1/icons/ico-social/";

        private readonly Dictionary<string, SocialNetworkSettings> _defaultSocialNetworks = new Dictionary<string, SocialNetworkSettings>()
        {
            { "facebook", new SocialNetworkSettings("https://www.facebook.com/sharer/sharer.php?u=[[URL]]", "#3b5998", $"{IMAGE_BASE_URL}facebook.png") },
            { "twitter", new SocialNetworkSettings("https://twitter.com/intent/tweet?url=[[URL]]", "#55acee", $"{IMAGE_BASE_URL}twitter.png") },
            { "google", new SocialNetworkSettings("https://plus.google.com/share?url=[[URL]]", "#dc4e41", $"{IMAGE_BASE_URL}google-plus.png") },
            { "pinterest", new SocialNetworkSettings("https://pinterest.com/pin/create/button/?url=[[URL]]&media=&description=", "#bd081c", $"{IMAGE_BASE_URL}pinterest.png") },
            { "linkedin", new SocialNetworkSettings("https://www.linkedin.com/shareArticle?mini=true&url=[[URL]]&title=&summary=&source=", "#0077b5", $"{IMAGE_BASE_URL}linkedin.png") },
            { "instagram", new SocialNetworkSettings(string.Empty, "#3f729b", $"{IMAGE_BASE_URL}instagram.png") },
            { "web", new SocialNetworkSettings(string.Empty, "#4BADE9", $"{IMAGE_BASE_URL}web.png") },
            { "snapchat", new SocialNetworkSettings(string.Empty, "#FFFA54", $"{IMAGE_BASE_URL}snapchat.png") },
            { "youtube", new SocialNetworkSettings(string.Empty, "#EB3323", $"{IMAGE_BASE_URL}youtube.png") },
            { "tumblr", new SocialNetworkSettings("https://www.tumblr.com/widgets/share/tool?canonicalUrl=[[URL]]", "#344356", $"{IMAGE_BASE_URL}tumblr.png") },
            { "github", new SocialNetworkSettings(string.Empty, "#000000", $"{IMAGE_BASE_URL}github.png") },
            { "xing", new SocialNetworkSettings("https://www.xing.com/app/user?op=share&url=[[URL]]", "#296366", $"{IMAGE_BASE_URL}xing.png") },
            { "vimeo", new SocialNetworkSettings(string.Empty, "#53B4E7", $"{IMAGE_BASE_URL}vimeo.png") },
            { "medium", new SocialNetworkSettings(string.Empty, "#000000", $"{IMAGE_BASE_URL}medium.png") },
            { "soundcloud", new SocialNetworkSettings(string.Empty, "#EF7F31", $"{IMAGE_BASE_URL}soundcloud.png") },
            { "dribbble", new SocialNetworkSettings(string.Empty, "#D95988", $"{IMAGE_BASE_URL}dribbble.png") },

            // LR: No-Share removes the sharing URL
            { "facebook-noshare", new SocialNetworkSettings("[[URL]]", "#3b5998", $"{IMAGE_BASE_URL}facebook.png") },
            { "twitter-noshare", new SocialNetworkSettings("[[URL]]", "#55acee", $"{IMAGE_BASE_URL}twitter.png") },
            { "google-noshare", new SocialNetworkSettings("[[URL]]", "#dc4e41", $"{IMAGE_BASE_URL}google-plus.png") },
            { "pinterest-noshare", new SocialNetworkSettings("[[URL]]", "#bd081c", $"{IMAGE_BASE_URL}pinterest.png") },
            { "linkedin-noshare", new SocialNetworkSettings("[[URL]]", "#0077b5", $"{IMAGE_BASE_URL}linkedin.png") },
            { "instagram-noshare", new SocialNetworkSettings("[[URL]]", "#3f729b", $"{IMAGE_BASE_URL}instagram.png") },
            { "web-noshare", new SocialNetworkSettings("[[URL]]", "#4BADE9", $"{IMAGE_BASE_URL}web.png") },
            { "snapchat-noshare", new SocialNetworkSettings("[[URL]]", "#FFFA54", $"{IMAGE_BASE_URL}snapchat.png") },
            { "youtube-noshare", new SocialNetworkSettings("[[URL]]", "#EB3323", $"{IMAGE_BASE_URL}youtube.png") },
            { "tumblr-noshare", new SocialNetworkSettings("[[URL]]", "#344356", $"{IMAGE_BASE_URL}tumblr.png") },
            { "github-noshare", new SocialNetworkSettings("[[URL]]", "#000000", $"{IMAGE_BASE_URL}github.png") },
            { "xing-noshare", new SocialNetworkSettings("[[URL]]", "#296366", $"{IMAGE_BASE_URL}xing.png") },
            { "vimeo-noshare", new SocialNetworkSettings("[[URL]]", "#53B4E7", $"{IMAGE_BASE_URL}vimeo.png") },
            { "medium-noshare", new SocialNetworkSettings("[[URL]]", "#000000", $"{IMAGE_BASE_URL}medium.png") },
            { "soundcloud-noshare", new SocialNetworkSettings("[[URL]]", "#EF7F31", $"{IMAGE_BASE_URL}soundcloud.png") },
            { "dribbble-noshare", new SocialNetworkSettings("[[URL]]", "#D95988", $"{IMAGE_BASE_URL}dribbble.png") },
        };

        private Dictionary<string, string> socialAttributes;

        public MjmlSocialElementComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
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
        }

        public override void SetupStyles()
        {
            socialAttributes = GetSocialAttributes();

            StyleLibraries.AddStyleLibrary("td", new Dictionary<string, string>() {
                { "padding", $"{GetAttribute("padding")}" },
                { "vertical-align", GetAttribute("vertical-align") }
            });

            StyleLibraries.AddStyleLibrary("table", new Dictionary<string, string>() {
                { "background-color", socialAttributes["background-color"] },
                { "border-radius", GetAttribute("border-radius") },
                { "width", socialAttributes["icon-size"] }
            });

            StyleLibraries.AddStyleLibrary("icon", new Dictionary<string, string>() {
                { "padding", GetAttribute("icon-padding") },
                { "font-size", "0" },
                { "height", !string.IsNullOrWhiteSpace(socialAttributes["icon-height"]) ? socialAttributes["icon-height"] : socialAttributes["icon-size"] },
                { "vertical-align", "middle" },
                { "width", socialAttributes["icon-size"] }
            });

            StyleLibraries.AddStyleLibrary("img", new Dictionary<string, string>() {
                { "border-radius", GetAttribute("border-radius") },
                { "display", "block" }
            });

            StyleLibraries.AddStyleLibrary("tdText", new Dictionary<string, string>() {
                { "vertical-align", "middle" },
                { "padding", GetAttribute("text-padding") }
            });

            StyleLibraries.AddStyleLibrary("text", new Dictionary<string, string>() {
                { "color", GetAttribute("color") },
                { "font-size", GetAttribute("font-size") },
                { "font-weight", GetAttribute("font-weight") },
                { "font-style", GetAttribute("font-style") },
                { "font-family", GetAttribute("font-family") },
                { "line-height", GetAttribute("line-height") },
                { "text-decoration", GetAttribute("text-decoration") },
            });
        }

        public Dictionary<string, string> GetSocialAttributes()
        {
            var socialNetworkName = GetAttribute("name");

            // LR: If the social network was not found then ignore it.
            if (!_defaultSocialNetworks.ContainsKey(socialNetworkName))
                return new Dictionary<string, string>();

            var socialNetwork = _defaultSocialNetworks[socialNetworkName];

            // LR: Generate the share href URL
            var href = GetAttribute("href");

            if (!string.IsNullOrEmpty(href) && !string.IsNullOrEmpty(socialNetwork.ShareUrl))
                href = socialNetwork.ShareUrl.Replace("[[URL]]", href);

            // LR: Generate the attributes - We could potentially move this to a class for speed.
            var attributes = new Dictionary<string, string>()
            {
                { "href", href },
                { "icon-size", GetAttribute("icon-size") },
                { "icon-height", GetAttribute("icon-size") },
                { "srcset", GetAttribute("srcset") },
                { "sizes", GetAttribute("sizes") },
                { "src", HasAttribute("src") ? GetAttribute("src") : socialNetwork.Src },
                { "background-color", HasAttribute("background-color") ? GetAttribute("background-color") : socialNetwork.BackgroundColor },
            };

            return attributes;
        }

        public string RenderContent(bool hasLink)
        {
            return $@"
                <td {HtmlAttributes(new Dictionary<string, string> {
                        { "style", "tdText" }
                    })}
                >
                    {(
                        hasLink ?
                            $@"<a {HtmlAttributes(new Dictionary<string, string> {
                                { "href", socialAttributes["href"] },
                                { "style", "text" },
                                { "rel", GetAttribute("rel") },
                                { "target", GetAttribute("target") }
                            })}>"
                            :
                            $@"<span {HtmlAttributes(new Dictionary<string, string> {
                                { "style", "text" },
                            })}>"
                    )}

                    {GetContent()}

                    {(hasLink ? "</a>" : "</span>")}
                </tr>
            ";
        }

        public override string RenderMjml()
        {
            var hasLink = HasAttribute("href");

            return $@"
                <tr {HtmlAttributes(new Dictionary<string, string> {
                        { "class", GetAttribute("css-class") }
                    })}
                >
                    <td {HtmlAttributes(new Dictionary<string, string> {
                            { "style", "td" }
                        })}
                    >
                        <table {HtmlAttributes(new Dictionary<string, string> {
                                { "border", "0" },
                                { "cellpadding", "0" },
                                { "cellspacing", "0" },
                                { "role", "presentation" },
                                { "style", "table" }
                            })}
                        >
                            <tr>
                                <td {HtmlAttributes(new Dictionary<string, string> {
                                        { "style", "icon" }
                                    })}
                                >
                                    {(
                                        hasLink ?
                                            $@"<a {HtmlAttributes(new Dictionary<string, string> {
                                                { "href", socialAttributes["href"] },
                                                { "rel", GetAttribute("rel") },
                                                { "target", GetAttribute("target") }
                                            })}>"
                                            : string.Empty
                                    )}

                                    <img {HtmlAttributes(new Dictionary<string, string> {
                                            { "alt", GetAttribute("alt") },
                                            { "title", GetAttribute("title") },
                                            { "height", !string.IsNullOrWhiteSpace(socialAttributes["icon-height"]) ? socialAttributes["icon-height"] : socialAttributes["icon-size"] },
                                            { "src", socialAttributes["src"] },
                                            { "style", "img" },
                                            { "width", GetAttribute("icon-size") },
                                            { "sizes", GetAttribute("sizes") },
                                            { "srcset", GetAttribute("srcset") }
                                        })}
                                    />

                                    {(hasLink ? $@"</a>" : string.Empty)}
                                </td>
                            </tr>
                        </table>
                    </td>

                    {(Children.Any() ? RenderContent(hasLink) : string.Empty)}

                </tr>
            ";
        }
    }
}