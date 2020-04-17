namespace BestService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Visit : BaseDeletableModel<int>
    {
        public int Count { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
