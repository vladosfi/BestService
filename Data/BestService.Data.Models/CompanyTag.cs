namespace BestService.Data.Models
{
    using BestService.Data.Common.Models;

    public class CompanyTag : BaseDeletableModel<int>
    {
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
