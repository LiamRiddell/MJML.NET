﻿using Mjml.Core;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mjml.Components
{
    public class MjmlRootComponent : MjmlBodyComponent
    {
        public MjmlRootComponent(XElement element) : base(element)
        {
        }

        public override Dictionary<string, string> SetAllowedAttributes()
        {
            return new Dictionary<string, string>
            {
            };
        }

        public override string RenderMjml()
        {
            return $@"
            <MjmlRootComponent type=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </MjmlRootComponent>";
        }
    }
}