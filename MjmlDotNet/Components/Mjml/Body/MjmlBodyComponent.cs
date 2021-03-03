﻿using AngleSharp.Dom;
using MjmlDotNet.Core.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Body
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-body/src/index.js
    internal class MjmlBodyComponent : BodyComponent
    {
        public MjmlBodyComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
            if (HasAttribute("Width"))
                HtmlSkeleton.ContainerWidth = GetAttribute("width");

            if (HasAttribute("background-color"))
                HtmlSkeleton.BackgroundColor = GetAttribute("background-color");
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Body.MjmlBody;
        }

        public override void SetupStyles()
        {
            StyleLibraries.AddStyleLibrary("div", new Dictionary<string, string>() {
                { "background-color", GetAttribute("background-color") },
            });
        }

        public override string RenderMjml()
        {
            return $@"
                <div {HtmlAttributes(new Dictionary<string, string> {
                        { "class", GetAttribute("css-class") },
                        { "style", "div" }
                     })}
                >
                    {this.RenderChildren()}
                </div>
            ";
        }
    }
}