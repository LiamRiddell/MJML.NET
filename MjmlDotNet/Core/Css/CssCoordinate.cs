namespace MjmlDotNet.Core.Css
{
    internal class CssCoordinate
    {
        public string X { get; set; }
        public string Y { get; set; }

        public CssCoordinate(string x, string y)
        {
            X = x;
            Y = y;
        }
    }
}