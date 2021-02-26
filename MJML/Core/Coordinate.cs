namespace MjmlDotNet.Core
{
    public class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

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