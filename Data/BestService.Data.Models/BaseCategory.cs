namespace BestService.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BestService.Data.Common.Models;

    public class BaseCategory : BaseDeletableModel<string>
    {
        public BaseCategory()
        {
            this.Categories = new HashSet<Category>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}
