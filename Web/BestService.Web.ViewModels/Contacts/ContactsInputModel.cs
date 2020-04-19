namespace BestService.Web.ViewModels.Contacts
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using BestService.Web.Infrastructure;

    public class ContactsInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        [DisplayName("Name")]
        public string FromName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your e-mail address")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        [Display(Name = "Your e-mail address")]
        public string SendersEmail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter subject for message")]
        [StringLength(50, ErrorMessage = "Subject must be between {2} and {1} simbols.", MinimumLength = 3)]
        [Display(Name = "Subject for message")]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your message")]
        [StringLength(10000, ErrorMessage = "Message must be at last {2} simbols.", MinimumLength = 10)]
        [Display(Name = "Message content")]
        public string Content { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
