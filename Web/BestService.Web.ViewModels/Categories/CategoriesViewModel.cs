namespace BestService.Web.ViewModels.Categories
{
    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string LogoImage { get; set; }

        public string ImageUri => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public int CompaniesCount { get; set; }

        public string Url => $"/c/{this.Name.Replace(' ', '-')}";
    }
}