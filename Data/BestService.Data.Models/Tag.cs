namespace BestService.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.CompanyTags = new HashSet<CompanyTag>();
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public virtual ICollection<CompanyTag> CompanyTags { get; set; }
    }
}
