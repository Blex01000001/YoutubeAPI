using HttpUtility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.PlayListContext.Models.Request;
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
            string endpoint = "https://www.googleapis.com/youtube/v3/playlists?part=snippet,status";
            var commentBody = new AddPlaylistRequest
            {
                snippet = new AddPlaylistRequest.Snippet
                {
                    title = title,
                    description = description
                },
                status = new AddPlaylistRequest.Status
                {
                    privacyStatus = privacyStatus
                }
            };
            var postResult = await _httpRequest.PostAsync<AddPlaylistResponse>(endpoint, commentBody);
            Console.WriteLine($"創建成功！ ID: {postResult.id}");

        }
        public async void DeleteAsync(string id)
        {
            string playlistId = id;
            string endpoint = $"https://www.googleapis.com/youtube/v3/playlists?id={playlistId}";
            await _httpRequest.DeleteAsync(endpoint);
            Console.WriteLine("播放清單已刪除！");
        }
    }
}
