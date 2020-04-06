﻿namespace BestService.Web.ViewModels.Companies
{
    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Ganss.XSS;

    public class CompanyDetailsViewModel : IMapFrom<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string LogoImage { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string UserUsername { get; set; }

        public string ImageUri => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public string CategoryId { get; set; }

        public long Visit { get; set; }
    }
}