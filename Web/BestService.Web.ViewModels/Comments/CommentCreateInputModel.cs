namespace BestService.Web.ViewModels.Comments
{
    public class CommentCreateInputModel
    {
        public string Content { get; set; }

        public byte Rating { get; set; }

        public int CompanyId { get; set; }

        public string UserId { get; set; }
    }
}
