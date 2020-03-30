namespace BestService.Web.ViewModels.Categories
{
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CompaniesInCategoryViewModel : IMapFrom<Company>
    {
        public string Name { get; set; }

        public float Rating { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 100 ? this.Description.Substring(0, 100) + "..." : this.Description;

        public string UserUsername { get; set; }
    }
}
