namespace BestService.Web.ViewModels.Categories
{
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CompaniesCount { get; set; }

        public string Url => $"/c/{this.Name.Replace(' ', '-')}";
    }
}