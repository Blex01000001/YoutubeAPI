using HttpUtility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeAPI.CommentContext.Models.Request;
using YoutubeAPI.CommentContext.Models.Reponse;

namespace YoutubeAPI.CommentContext
{
    public class Comment
    {
        private IHttpRequest _httpRequest;
        public Comment(IHttpRequest httpRequest)
        {
            this._httpRequest = httpRequest;
        }
        public async void SubmitCommentAsync(string parentId, string textOriginal)
        {
            string endpoint = "https://www.googleapis.com/youtube/v3/comments?part=snippet";
            var commentBody = new CommentRequest
            {
                snippet = new CommentSnippet
                {
                    parentId = parentId,
                    textOriginal = textOriginal
                }
            };
            var postResult = await _httpRequest.PostAsync<CommentResponse>(endpoint, commentBody);
            Console.WriteLine($"留言成功！留言 ID: {postResult.id}");
        }
        public async void ModifyCommentAsync(string id, string textOriginal)
        {
            string updateEndpoint = "https://www.googleapis.com/youtube/v3/comments?part=snippet";
            var updateBody = new EditCommentRequest
            {
                id = id,
                snippet = new EditCommentRequest.Snippet
                {
                    textOriginal = textOriginal
                }
            };
            var updateResult = await _httpRequest.PutAsync<EditCommentResponse>(updateEndpoint, updateBody);
            Console.WriteLine($"修改成功！新的內容: {updateResult.snippet.textOriginal}");
        }
        public async void DeleteCommentAsync(string id)
        {
            string commentIdToDelete = id;
            string deleteEndpoint = $"https://www.googleapis.com/youtube/v3/comments?id={commentIdToDelete}";
            await _httpRequest.DeleteAsync(deleteEndpoint);
            Console.WriteLine($"評論已刪除 commentId: {commentIdToDelete}");
        }

    }
}
