namespace BestService.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    public class CompanyViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<CompaniesDetailsViewModel> Companies { get; set; }
    }
}
