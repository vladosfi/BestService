namespace BestService.Web.ViewModels.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using BestService.Web.ViewModels.Comments;
    using Ganss.XSS;

    public class CompanyDetailsViewModel : IMapFrom<Company>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoImage { get; set; }

        public string Description { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Description);

        public string ImageUri => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public DateTime CreatedOn { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<string> TagCloud { get; set; }

        public string CategoryUrl => $"/c/{this.CategoryName.Replace(' ', '-')}";

        public long Visit { get; set; }

        public bool IsContentEditor { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double ArverageStars => Math.Round(this.RatingAvg, 1, MidpointRounding.AwayFromZero);

        public double RatingAvg { get; set; }

        public int Rating => (int)Math.Round(this.RatingAvg);

        public bool IsRateAllowed { get; set; }

        public virtual ICollection<Rate> Ratings { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Company, CompanyDetailsViewModel>()
                .ForMember(x => x.RatingAvg, options =>
                {
                    options.MapFrom(c => c.Ratings.Average(r => r.Stars));
                });
        }
    }
}
