namespace BestService.Web.ViewModels.Testimonial
{
    using System.Net;
    using System.Text.RegularExpressions;

    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class TestimonialViewModel : IMapFrom<Comment>
    {
        private const int ContentLenght = 80;
        private const int StartIndex = 0;

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                string content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]*>", string.Empty));
                return content?.Length > ContentLenght ? content.Substring(StartIndex, ContentLenght) + "..." : content;
            }
        }

        public virtual ApplicationUser User { get; set; }

        public string UserName => this.User.UserName;
    }
}