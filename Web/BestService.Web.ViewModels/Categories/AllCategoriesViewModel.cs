namespace BestService.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel
    {
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
