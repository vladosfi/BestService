namespace BestService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(3000)]
        public string Content { get; set; }

        [Required]
        public byte Rating { get; set; }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
