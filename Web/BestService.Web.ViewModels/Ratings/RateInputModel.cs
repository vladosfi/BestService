namespace BestService.Web.ViewModels.Ratings
{
    using System.ComponentModel.DataAnnotations;

    public class RateInputModel
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }
    }
}
