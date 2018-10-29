using System;
using System.Linq;
using AngleSharp.Dom.Html;

namespace ConsoleApp1
{
    public class StitchSuParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument doc)
        {
            var items = doc.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName == "set").Select(item => item.OuterHtml).ToArray();
            var items2 = doc.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName == "set").Select(item => item.Style).ToArray();
            return items;
        }
    }
}
