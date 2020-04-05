namespace BestService.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Company : BaseDeletableModel<int>
    {
        public Company()
        {
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<Rate>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }

        public string LogoImage { get; set; }

        public string OfficialSite { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rate> Ratings { get; set; }

        [Required]
        public int VisitId { get; set; }

        public Visit Visit { get; set; }
    }
}
