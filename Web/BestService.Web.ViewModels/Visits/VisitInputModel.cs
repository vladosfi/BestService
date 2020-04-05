namespace BestService.Web.ViewModels.Visits
{
    using System.ComponentModel.DataAnnotations;

    public class VisitInputModel
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public long VisitCount { get; set; }
    }
}
