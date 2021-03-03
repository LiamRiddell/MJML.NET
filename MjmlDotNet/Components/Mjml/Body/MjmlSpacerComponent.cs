﻿using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/d4c6ea0744e05c928044108c3117c16a9c4110fe/packages/mjml-spacer/src/index.js
    internal class MjmlSpacerComponent : BodyComponent
    {
        public MjmlSpacerComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlSpacer;
        }

        public override void SetupStyles()
        {
            // LR: Add styles
            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "height", GetAttribute("height") }
            });
        }

        public override string RenderMjml()
        {
            var height = GetAttribute("height");

            if (string.IsNullOrWhiteSpace(height))
                height = "20px";

            return $@"
                {TagHelpers.ConditionalTag($@"
                    <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                        <tr>
                            <td height=""{CssUnitParser.Parse(height).Value}"" style=""vertical-align:top;height:{height};"">
                ")}

                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "style", "div"}
                     })}
                >
                    &nbsp;
                </div>

                {TagHelpers.ConditionalTag($@"
                            </td>
                        </tr>
                    </table>
                ")}
            ";
        }
    }
}