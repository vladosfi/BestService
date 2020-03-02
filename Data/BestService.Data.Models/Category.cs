namespace BestService.Data.Models
{
    using BestService.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
