namespace BestService.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    public class CompanyViewModel
    {
        public IEnumerable<CompaniesDetailsViewModel> Companies { get; set; }
    }
}
