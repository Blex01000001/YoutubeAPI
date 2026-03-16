using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeAPI.CommentContext.Models.Request
{
    public class EditCommentRequest
    {
        public string id { get; set; }
        public Snippet snippet { get; set; }
        public class Snippet
        {
            public string textOriginal { get; set; }
        }
    }
}
