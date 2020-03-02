namespace BestService.Data.Models
{
    using System.Collections.Generic;

    using BestService.Data.Common.Models;

    public class BaseCategory : BaseDeletableModel<string>
    {
        public BaseCategory()
        {
            this.Categories = new HashSet<Category>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
