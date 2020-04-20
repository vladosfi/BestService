namespace BestService.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int CompaniesCount { get; set; }

        public int CommentsCount { get; set; }

        public int RateCount { get; set; }

        public int Subscribers { get; set; }

        public IEnumerable<IndexCompanyDetailsViewModel> MostRecentCompanies { get; set; }

        public IEnumerable<IndexCompanyDetailsViewModel> MostCommentedCompanies { get; set; }
    }
}
