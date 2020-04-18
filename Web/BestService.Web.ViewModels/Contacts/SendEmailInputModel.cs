namespace BestService.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class SendEmailInputModel
    {
        [Required]
        [RegularExpression(@"[^@]+@[^\.]+\..+")]
        public string SendersEmail { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        [RegularExpression("[A-Z][a-z]+ [A-Z][a-z]+")]
        public string FromName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Subject { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(3000)]
        public string HtmlContent { get; set; }
    }
}
