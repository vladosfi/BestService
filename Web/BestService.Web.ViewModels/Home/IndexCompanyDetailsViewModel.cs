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
                return description?.Length > 120 ? description.Substring(0, 120) + " [...]" : description;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string Month => this.CreatedOn.ToString("MMMM", CultureInfo.InvariantCulture);

        public string Created => this.CreatedOn.ToUniversalTime().ToShortDateString();

        public string UserUsername { get; set; }

        public Category Category { get; set; }

        public int Rating { get; set; }

        public int VisitCount => this.Visits.Where(c => c.CompanyId == this.Id).Sum(v => v.Count);

        public virtual ICollection<Visit> Visits { get; set; }

        public virtual ICollection<Rate> Ratings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, IndexCompanyDetailsViewModel>()
                .ForMember(x => x.Rating, options =>
               {
                   options.MapFrom(c => c.Ratings.Sum(r => r.Stars));
                });
        }
    }
}
