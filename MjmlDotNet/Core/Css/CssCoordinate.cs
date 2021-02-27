namespace MjmlDotNet.Core.Css
{
    public class CssCoordinate
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