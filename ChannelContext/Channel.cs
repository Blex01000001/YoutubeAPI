using HttpUtility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.CommentContext.Models.Reponse;
using YoutubeAPI.VideoContext.Models.Reponse;
using YoutubeAPI.ChannelContext.Models.Reponse;

namespace YoutubeAPI.ChannelContext
{
    public class Channel
    {
        private IHttpRequest _httpRequest;

        public Channel(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }
        public async void SubscribeAsync(string channelId)
        {

        }
        public async void UnSubscribeAsync(string channelId)
        {

        }
        public async void SearchAsync(string q)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "snippet" },
                { "q", q },
                { "type", "channel" },
                { "maxResults", "25" }
            };
            var result = await _httpRequest.GetAsync<SearchListResponse>("search", parameters);

            for (int i = 0; i < result.items.Length; i++)
            {
                Console.WriteLine($"#{i} {result.items[i].snippet.title} {result.items[i].snippet.description}\n");
            }

        }
        public async void SubscribeNumberAsync(string channelId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "statistics" },
                { "id", channelId }
            };
            var result = await _httpRequest.GetAsync<ChannelListResponse>("channels", parameters);
            Console.WriteLine($"查詢成功！ subscriber Count: {result.items[0].statistics.subscriberCount}");
        }
    }
}
