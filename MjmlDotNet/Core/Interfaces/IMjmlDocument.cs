namespace MjmlDotNet.Core.Interfaces
{
    public interface IMjmlDocument
    {
        string Compile(bool prettify = false);

        string PrettifyHtml(string content);

        string MinifyHtml(string content);
    }
}