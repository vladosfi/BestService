namespace BestService.Data.Models
{
    using System.Collections.Generic;

    using BestService.Data.Common.Models;

    public class Company : BaseDeletableModel<string>
    {
        public Company()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public string Image { get; set; }

        public string OfficialSite { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
