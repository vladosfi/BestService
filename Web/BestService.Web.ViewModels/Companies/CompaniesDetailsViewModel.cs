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

    public class CompaniesDetailsViewModel : IMapFrom<Company>, IMapTo<Company>, IHaveCustomMappings
    {
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
                return description?.Length > 150 ? description.Substring(0, 150) + "..." : description;
            }
        }

        // public string Created => this.CreatedOn.ToUniversalTime().ToShortDateString();
        public DateTime CreatedOn { get; set; }

        public string UserUsername { get; set; }

        public Category Category { get; set; }

        public int Rating { get; set; }

        public int VisitId { get; set; }

        public virtual ICollection<Rate> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, CompaniesDetailsViewModel>()
                .ForMember(x => x.Rating, options =>
               {
                   options.MapFrom(c => c.Ratings.Sum(r => r.Stars));
                });
        }
    }
}
