namespace BestService.Web.ViewModels.Companies
{
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CategoryDropdownViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}