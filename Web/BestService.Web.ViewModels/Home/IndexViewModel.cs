namespace BestService.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }

        public IEnumerable<IndexCompanyDetailsViewModel> MostRecentCompanies { get; set; }

        public IEnumerable<IndexCompanyDetailsViewModel> MostCommentedCompanies { get; set; }
    }
}
