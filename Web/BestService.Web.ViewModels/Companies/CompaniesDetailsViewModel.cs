namespace BestService.Web.ViewModels.Companies
{
    using System.Net;
    using System.Text.RegularExpressions;

    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CompaniesDetailsViewModel : IMapFrom<Company>
    {
        public string Name { get; set; }

        public float Rating { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                string description = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]>", string.Empty));
                return description?.Length > 150 ? description.Substring(0, 150) + "..." : description;
            }
        }

        public string UserUsername { get; set; }
    }
}