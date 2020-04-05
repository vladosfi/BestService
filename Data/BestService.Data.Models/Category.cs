namespace BestService.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Companies = new HashSet<Company>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }

        public string Link { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
