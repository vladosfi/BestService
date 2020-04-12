namespace BestService.Web.ViewModels.RecentCompanies
{
    using System.Collections.Generic;

    public class RecentCompanyViewModel
    {
        public IEnumerable<RecentCompaniesCategoriesViewModel> Companies { get; set; }
    }
}
