namespace BestService.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Companies = new HashSet<Company>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }

        public string Link { get; set; }

        public string BaseCategoryId { get; set; }

        public virtual BaseCategory BaseCategory { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
