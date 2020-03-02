namespace BestService.Data.Models
{
    using BestService.Data.Common.Models;

    public class Company : BaseDeletableModel<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public string Image { get; set; }

        public string OfficialSite { get; set; }

        public Category Category { get; set; }
    }
}
