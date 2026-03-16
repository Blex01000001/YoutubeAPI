using HttpUtility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.PlayListContext.Models.Reponse;

namespace YoutubeAPI.PlayListContext
{
    public class PlayList
    {
        private IHttpRequest _httpRequest;
        public PlayList(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }
        public async void SearchAsync(string channelId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "snippet,contentDetails" },
                { "channelId", channelId }
            };
            var result = await _httpRequest.GetAsync<PlaylistListResponse>("playlists", parameters);

            for (int i = 0; i < result.items.Length; i++)
            {
                Console.WriteLine($"#{i} {result.items[i].snippet.title} {result.items[i].snippet.description}\n");
            }
        }
        public async void CreateAsync(string title, string description, string privacyStatus)
        {

        }
        public async void DeleteAsync(string id)
        {

        }
    }
}
