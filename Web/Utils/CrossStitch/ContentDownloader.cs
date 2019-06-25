using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Utils.CrossStitch
{
    public abstract class ContentDownloader
    {
        protected const int ItemsPerPage = 50;

        protected HtmlParser Parser { get; private set; }

        protected ContentDownloader()
        {
            Parser = new HtmlParser();
        }

        protected async Task<IHtmlDocument> DownloadContent(Uri uri)
        {
            var content = await Download(uri);
            return await Parser.ParseAsync(content);
        }

        private async Task<string> Download(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri.AbsoluteUri);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            return string.Empty;
        }
    }
}
