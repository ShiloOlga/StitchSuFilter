using AngleSharp.Dom;
using AngleSharp.Parser.Css;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Download().Wait();

        }

        private static async Task Download()
        {
            var uri = new Uri("https://www.stitch.su/patterns?favch=4&page=3&lim=50");
            using (var client = new HttpClient())
            {
                //await ExtractCssContent(client);
                var response = await client.GetAsync(uri.AbsoluteUri);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var domParser = new HtmlParser();
                    using (var document = await domParser.ParseAsync(content))
                    {
                        var doc = new XmlDocument();

                        var htmlRoot = doc.CreateElement("html");
                        doc.AppendChild(htmlRoot);

                        var htmlHead = doc.CreateElement("head");
                        htmlHead.AppendChild(doc.CreateTextNode("<link rel=\"stylesheet\" href=\"c:/Users/olga.shilo/source/repos/ConsoleApp1/ConsoleApp1/bin/Debug/main.css?52\" media=\"all\">"));
                        htmlRoot.AppendChild(htmlHead);

                        var body = doc.CreateElement("body");
                        var divNodes = document.All.Where(item => item.LocalName == "div").Where(item => item.ClassName != null && item.ClassName == "set");
                        foreach (var divNode in divNodes)
                        {
                            //HtmlProcessingUtility.ReplaceAllRelativeLinks(divNode, uri);
                            var model = StitchSuPatternModel.Parse(divNode, uri);
                            Console.WriteLine(model.ToString());
                        }

                        //var items = divNodes.Select(item => item.OuterHtml).ToArray();
                        //foreach (var node in items)
                        //{
                        //    body.AppendChild(doc.CreateTextNode(node));
                        //}
                        //htmlRoot.AppendChild(body);
                        //File.WriteAllText("result.html", WebUtility.HtmlDecode(doc.OuterXml));
                    }
                }
            }
            Console.ReadKey();
        }

        private static async Task ExtractCssContent(HttpClient client)
        {
            var cssParser = new CssParser();
            var css = await client.GetAsync("https://www.stitch.su/css/main.css");
            var cssContent = await css.Content.ReadAsStringAsync();
            var cssParsed = cssParser.ParseStylesheet(cssContent);
            File.WriteAllText("main.css", cssContent);
        }

        private static IEnumerable<INode> GetInnerNodesByCondition(INode root, Func<INode, bool> selectorFunc)
        {
            var list = new List<INode>();
            if (root.HasChildNodes)
            {
                foreach (var child in root.ChildNodes)
                {
                    list.AddRange(GetInnerNodesByCondition(child, selectorFunc));
                }
            }
            if (selectorFunc(root))
            {
                list.Add(root);
            }
            return list;
        }
    }
}
