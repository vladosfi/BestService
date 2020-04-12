namespace BestService.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    public class CompanyViewModel
    {
        public IEnumerable<IndexCompanyDetailsViewModel> Companies { get; set; }
    }
}
