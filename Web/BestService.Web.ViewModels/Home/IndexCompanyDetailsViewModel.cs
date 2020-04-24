namespace BestService.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class IndexCompanyDetailsViewModel : IMapFrom<Company>, IMapTo<Company>, IHaveCustomMappings
    {
        private const int DescriptionLenght = 120;
        private const int StartIndex = 0;

        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName
        {
            get
            {
                return this.Name?.Length > 25 ? this.Name.Substring(0, 25) + "..." : this.Name;
            }
        }

        public string LogoImage { get; set; }

        public string LogoImagePath => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                string description = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]*>", string.Empty));
                return description?.Length > DescriptionLenght ? description.Substring(StartIndex, DescriptionLenght) + " [...]" : description;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string Month => this.CreatedOn.ToString("MMMM", CultureInfo.InvariantCulture);

        public string Created => this.CreatedOn.ToUniversalTime().ToShortDateString();

        public string UserUsername { get; set; }

        public Category Category { get; set; }

        public double Ratings { get; set; }

        public double ArverageStars => Math.Round(this.Ratings, 1, MidpointRounding.AwayFromZero);

        public int VisitsCount { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, IndexCompanyDetailsViewModel>()
                .ForMember(x => x.Ratings, options =>
               {
                   options.MapFrom(c => c.Ratings.Average(r => r.Stars));
               });
        }
    }
}
