using System;
using System.Text.RegularExpressions;

namespace MjmlDotNet.Helpers
{
    public class CssParsedUnit
    {
        public string Unit { get; set; }
        public float Value { get; set; }

        public CssParsedUnit(string unit, float value)
        {
            Unit = unit;
            Value = value;
        }

        public override string ToString() => $"{Value}{Unit}";
    }

    public static class CssUnitParser
    {
        public static CssParsedUnit Parse(string cssValue)
        {
            Regex unitRegex = new Regex(@"([0-9.,]*)([^0-9]*)$");
            var match = unitRegex.Match(cssValue);

            if (!match.Success)
                throw new Exception($"CssWidthParser could not parse {cssValue} due to invalid format");

            var widthValue = match.Groups[1].Value;
            var widthUnit = match.Groups.Count != 3 ? "px" : match.Groups[2].Value;

            switch (widthUnit.ToLower())
            {
                case "px":
                    return new CssParsedUnit(widthUnit, int.Parse(widthValue));

                case "%":
                    return new CssParsedUnit(widthUnit, float.Parse(widthValue));

                default:
                    return new CssParsedUnit(widthUnit, int.Parse(widthValue));
            }
        }
    }
}