using HttpUtility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.CommentContext.Models.Reponse;
using YoutubeAPI.VideoContext.Models.Reponse;

namespace YoutubeAPI.VideoContext
{
    public class Video
    {
        private IHttpRequest _httpRequest;
        public Video(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }
        public async void UploadVideoAsync()
        {

        }
        public async void RatingAsync(string videoId)
        {

        }
        public async void SearchAsync(string q)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "snippet" },
                { "q", q },
                { "type", "video" },
                { "maxResults", "25" }
            };
            var result = await _httpRequest.GetAsync<SearchListResponse>("search", parameters);

            for (int i = 0; i < result.items.Length; i++)
            {
                Console.WriteLine($"#{i} {result.items[i].snippet.title} {result.items[i].snippet.description}\n");
            }
        }
        public async void SearchInfoAsync(string videoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "snippet,statistics,contentDetails" },
                { "id", videoId }
            };
            var result = await _httpRequest.GetAsync<VideoListResponse>("videos", parameters);

            for (int i = 0; i < result.items.Length; i++)
            {
                Console.WriteLine($"{result.items[i].snippet.title} {result.items[i].snippet.description}\n");
            }
        }
        public async void SearchCommentAsync(string videoId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "snippet,replies" },
                { "videoId", videoId }
            };
            var result = await _httpRequest.GetAsync<CommentThreadListResponse>("commentThreads", parameters);

            for (int i = 0; i < result.items.Length; i++)
            {
                Console.WriteLine($"#{i} {result.items[i].snippet.topLevelComment.snippet.textDisplay}\n");
            }
        }
        public async void SearchSubCommentAsync(string parentId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "part", "snippet" },
                { "parentId", parentId }
            };
            var result = await _httpRequest.GetAsync<CommentListResponse>("comments", parameters);

            for (int i = 0; i < result.items.Length; i++)
            {
                Console.WriteLine($"#{i} {result.items[i].snippet.textDisplay}\n");
            }
        }

    }
}
