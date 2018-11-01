using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Utils.CrossStitch
{
    public abstract class ContentDownloader
    {
        public async Task<string> Download(Uri uri)
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
