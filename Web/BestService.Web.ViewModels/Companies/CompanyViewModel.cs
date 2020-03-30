namespace BestService.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CompanyViewModel
    {
        public IEnumerable<CompaniesDetailsViewModel> Companies { get; set; }
    }
}
