namespace BestService.Data.Models
{
    using BestService.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
