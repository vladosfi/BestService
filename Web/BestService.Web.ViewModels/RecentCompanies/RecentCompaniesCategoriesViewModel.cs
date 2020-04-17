namespace BestService.Web.ViewModels.RecentCompanies
{
    using System;
    using System.Collections.Generic;

    using BestService.Common;
    using BestService.Data.Models;
    using BestService.Services.Mapping;

    public class RecentCompaniesCategoriesViewModel : IMapFrom<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string LogoImage { get; set; }

        public string LogoImagePath => GlobalConstants.CloudinaryUploadDir + this.LogoImage;

        public DateTime CreatedOn { get; set; }

        public string CreatedShort => this.CreatedOn.ToShortDateString();

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserName => this.User.UserName;
    }
}
