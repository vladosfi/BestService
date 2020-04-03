namespace BestService.Web.ViewModels.Companies
{
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Link { get; set; }
    }
}
