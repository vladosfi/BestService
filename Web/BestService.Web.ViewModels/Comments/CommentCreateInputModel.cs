namespace BestService.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentCreateInputModel
    {
        [Required]
        [Display(Name ="Content:")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Rating:")]
        public byte Rating { get; set; }

        public int ParentId { get; set; }

        public int CompanyId { get; set; }
    }
}
