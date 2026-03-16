using HttpUtility.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.ChannelContext;
using YoutubeAPI.CommentContext;
using YoutubeAPI.PlayListContext;
using YoutubeAPI.VideoContext;


namespace YoutubeAPI
{
    public class YoutubeContext
    {
        public Channel Channel {  get; set; }
        public Video Video { get; set; }
        public Comment Comment { get; set; }
        public PlayList PlayList { get; set; }

        private IHttpRequest httpRequest;

        public YoutubeContext() { 
        
            this.httpRequest = new HttpRequest();
            this.Channel = new Channel(this.httpRequest);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            httpRequest = new HttpRequest();
            httpRequest.BaseUrl = "https://www.googleapis.com/youtube/v3/";
            httpRequest.Token = configuration["YouTubeApi:AccessToken"];

            Video = new Video(httpRequest);
            Channel = new Channel(httpRequest);
            Comment = new Comment(httpRequest);
            PlayList = new PlayList(httpRequest);
        }
    }
}
