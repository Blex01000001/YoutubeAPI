using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            YoutubeContext context = new YoutubeContext();
            //context.Video.SearchAsync("2330");
            //context.Channel.SearchAsync("2330");
            //context.Video.SearchInfoAsync("Hoixgm4-P4M");
            //context.Video.SearchCommentAsync("Hoixgm4-P4M");
            //context.Video.SearchSubCommentAsync("UgzcBaEoaVe1wd3rzNJ4AaABAg");
            //context.PlayList.SearchAsync("UC5nwNW4KdC0SzrhF9BXEYOQ");
            //context.Channel.SubscribeNumberAsync("UC5nwNW4KdC0SzrhF9BXEYOQ");
            //context.Comment.SubmitCommentAsync("UgzcBaEoaVe1wd3rzNJ4AaABAg", "0316");
            //context.Comment.ModifyCommentAsync("UgzcBaEoaVe1wd3rzNJ4AaABAg.9_QY0W4wBszAUQH-F8pgB1", "0316-2");
            context.Comment.DeleteCommentAsync("UgzcBaEoaVe1wd3rzNJ4AaABAg.9_QY0W4wBszAUQH-F8pgB1");




            Console.ReadKey();
        }
    }
}
