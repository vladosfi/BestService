namespace BestService.Web.ViewModels.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using BestService.Web.ViewModels.Categories;

    public class CompaniesDetailsViewModel : IMapFrom<Company>, IMapTo<Company>, IHaveCustomMappings
    {
        private const int DescriptionLenght = 120;
        private const int StartIndex = 0;

        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoImage { get; set; }

        public string LogoImagePath => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                string description = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]*>", string.Empty));
                return description?.Length > DescriptionLenght ? description.Substring(StartIndex, DescriptionLenght) + "..." : description;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string UserUsername { get; set; }

        public CategoriesViewModel Category { get; set; }

        public double RatingAvg { get; set; }

        public double ArverageStars => Math.Round(this.RatingAvg, 1, MidpointRounding.AwayFromZero);

        public int Rating => (int)Math.Round(this.RatingAvg);

        public virtual ICollection<Rate> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int VisitsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, CompaniesDetailsViewModel>()
                .ForMember(x => x.RatingAvg, options =>
                {
                    options.MapFrom(c => c.Ratings.Average(r => r.Stars));
                });
        }
    }
}
