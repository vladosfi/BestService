namespace BestService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Rate : BaseModel<int>
    {
        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Stars { get; set; }

        public int UserRateCount { get; set; }
    }
}
