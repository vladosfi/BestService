namespace BestService.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
