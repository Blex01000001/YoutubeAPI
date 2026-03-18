//using HttpUtility.Models.DTOs;
using HttpUtility.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YoutubeAPI.CommentContext.Models.Reponse;
using YoutubeAPI.VideoContext.Models.Reponse;
using YoutubeAPI.VideoContext.Models.Request;

namespace YoutubeAPI.VideoContext
{
    public class Video
    {
        private IHttpRequest _httpRequest;
        public Video(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }
        public async void UploadVideoAsync(string videoPath)
        {
            string updateEndpoint = "https://www.googleapis.com/upload/youtube/v3/videos?uploadType=resumable&part=snippet,status";
            // 建立 Multipart 容器，YouTube API 要求類型為 "multipart/related"
            var fileStream = new FileStream(videoPath, FileMode.Open, FileAccess.Read);
            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");
            MultipartContent multipartContent = new MultipartContent("related", "TEST_BOUNDARY_STRING");

            var updateBody = new UploadVideoRequest
            {
                snippet = new UploadVideoRequest.Snippet
                {
                    title = "Video title",
                    description = "Video description 0318",
                    categoryId = "22"
                },
                status = new UploadVideoRequest.Status
                {
                    privacyStatus = "public"
                }
            };
            // 第一部分：JSON 中繼資料 (Snippet, Status)
            var json = JsonConvert.SerializeObject(updateBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            multipartContent.Add(content);
            multipartContent.Add(streamContent);

            // 發送 POST 請求
            string url = "https://www.googleapis.com/upload/youtube/v3/videos?uploadType=multipart&part=snippet,status";
            var result = await _httpRequest.PostAsync<UploadVideoRequest>(url, multipartContent);

            Console.WriteLine($"title {result.snippet.title}");
        }
        public async void RatingAsync(string videoId, string rating)
        {
            var input = new Dictionary<string, string> { };
            string endpoint = $"https://www.googleapis.com/youtube/v3/videos/rate?id={videoId}&rating={rating}";
            await _httpRequest.PostAsync(endpoint, input);
            Console.WriteLine("成功為影片按讚！");
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
