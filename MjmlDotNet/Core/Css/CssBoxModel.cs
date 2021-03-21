namespace MjmlDotNet.Core.Css
{
    internal class CssBoxModel
    {
        public float TotalWidth { get; set; }
        public float BorderWidth { get; set; }
        public float PaddingWidth { get; set; }
        public float BoxWidth { get; set; }

        public CssBoxModel(float totalWidth, float borderWidth, float paddingWidth, float boxWidth)
        {
            TotalWidth = totalWidth;
            BorderWidth = borderWidth;
            PaddingWidth = paddingWidth;
            BoxWidth = boxWidth;
        }
    }
}