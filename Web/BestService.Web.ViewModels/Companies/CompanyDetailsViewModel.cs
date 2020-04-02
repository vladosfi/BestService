namespace BestService.Web.ViewModels.Companies
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class CompanyDetailsViewModel : IMapFrom<Company>
    {
        public string Name { get; set; }

        public float Rating { get; set; }

        public string LogoImage { get; set; }

        public string Description { get; set; }

        public string DecodedDescriptin
        {
            get
            {
                var description = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]*>", string.Empty));
                return description;
            }
        }

        public string UserUsername { get; set; }

        public string ImageUri => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public string CategoryId { get; set; }
    }
}
