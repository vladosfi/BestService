namespace BestService.Web.ViewModels.Subscribes
{
    using System.ComponentModel.DataAnnotations;

    public class SubscribeInputModel
    {
        [Required]
        //[EmailAddress]
        public string Email { get; set; }
    }
}
