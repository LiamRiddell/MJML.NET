﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MJML
{
    public class MjmlComponent : MjmlBaseComponent
    {
        public MjmlComponent(XElement element) : base(element)
        {
        }

        public override string RenderMjml()
        {
            return $@"
            <MjmlComponent type=""{Element.Name.LocalName}"">
                {this.RenderChildren()}
            </MjmlComponent>";
        }
    }
}