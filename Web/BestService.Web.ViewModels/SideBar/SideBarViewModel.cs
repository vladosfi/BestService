namespace BestService.Web.ViewModels.SideBar
{
    using System.Collections.Generic;

    public class SideBarViewModel
    {
        public IEnumerable<SideBarCategoriesViewModel> Categories { get; set; }
    }
}
