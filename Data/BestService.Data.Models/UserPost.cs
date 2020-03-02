namespace BestService.Data.Models
{
    using System.Collections.Generic;

    using BestService.Data.Common.Models;

    public class UserPost : BaseDeletableModel<string>
    {
        public UserPost()
        {
            this.Companies = new HashSet<Company>();
        }

        public ApplicationUser User { get; set; }

        public string PostText { get; set; }

        public IEnumerable<Company> Companies { get; set; }
    }
}
