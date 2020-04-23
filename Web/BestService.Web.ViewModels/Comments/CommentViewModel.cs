namespace BestService.Web.ViewModels.Comments
{
    using System;

    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Ganss.XSS;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParrentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public byte Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string UserProfileImage { get; set; }
    }
}
