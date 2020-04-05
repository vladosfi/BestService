namespace BestService.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCompanyViewModel : IMapFrom<Company>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Company Name:")]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Official Site:")]
        public string OfficialSite { get; set; }

        [Required]
        [Display(Name = "Category:")]
        public string CategoryId { get; set; }

        [Display(Name = "Logo Image:")]
        public string LogoImage { get; set; }

        [Display(Name = "Logo Image:")]
        public IFormFile LogoImageFile { get; set; }

        public string UserId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
