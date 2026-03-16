using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeAPI.ChannelContext.Models.Reponse
{
    public class ChannelListResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public Pageinfo pageInfo { get; set; }
        public Item[] items { get; set; }

        public class Pageinfo
        {
            public int totalResults { get; set; }
            public int resultsPerPage { get; set; }
        }

        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Statistics
        {
            public string viewCount { get; set; }
            public string subscriberCount { get; set; }
            public bool hiddenSubscriberCount { get; set; }
            public string videoCount { get; set; }
        }

    }
}
