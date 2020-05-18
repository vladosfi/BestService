namespace BestService.Data.Models
{
    using System.Collections.Generic;

    using BestService.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.CompanyTags = new HashSet<CompanyTag>();
        }

        public string Title { get; set; }

        public virtual ICollection<CompanyTag> CompanyTags { get; set; }
    }
}
