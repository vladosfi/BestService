namespace BestService.Web.ViewModels.SideBar
{
    using System.Collections.Generic;

    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class SideBarCategoriesViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}