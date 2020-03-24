namespace BestService.Web.ViewModels.Home
{
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CompaniesCount { get; set; }

        public string Url => $"/c/{this.Name.Replace(' ', '-')}";
    }
}
