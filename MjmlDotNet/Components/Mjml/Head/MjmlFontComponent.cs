﻿using AngleSharp.Dom;
using MjmlDotNet.Components.Attributes;
using MjmlDotNet.Core.Components;
using MjmlDotNet.Core.Helpers;
using System.Collections.Generic;

namespace MjmlDotNet.Components.Mjml.Head
{
    // https://github.com/mjmlio/mjml/blob/246df840f4d0fcd812e51ca55bd6bef6592cb0e6/packages/mjml-head-font/src/index.js
    internal class MjmlFontComponent : HeadComponent
    {
        public MjmlFontComponent(IElement element, BaseComponent parent) : base(element, parent)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return GlobalDefaultAttributes.Head.MjmlFont;
        }

        public override void Handler()
        {
            if (HasAttribute("name") && HasAttribute("href"))
                HtmlSkeleton.AddFont(GetAttribute("name"), GetAttribute("href"));
        }
    }
}