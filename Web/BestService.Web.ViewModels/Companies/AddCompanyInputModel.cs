namespace BestService.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http.Internal;

    public class AddCompanyInputModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Company Name:")]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Upload Image:")]
        public string Image { get; set; }

        [Display(Name = "Official Site:")]
        public string OfficialSite { get; set; }

        [Required]
        [Display(Name = "Category:")]
        public string CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
