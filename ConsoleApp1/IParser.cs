using AngleSharp.Dom.Html;

namespace ConsoleApp1
{
    public interface IParser<T> where T: class
    {
        T Parse(IHtmlDocument doc);
    }
}
