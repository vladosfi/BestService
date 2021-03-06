﻿namespace BestService.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Company : BaseDeletableModel<int>
    {
        public Company()
        {
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<Rate>();
            this.CompanyTags = new HashSet<Tag>();
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

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rate> Ratings { get; set; }

        public IEnumerable<Visit> Visits { get; set; }

        public virtual ICollection<Tag> CompanyTags { get; set; }
    }
}
