namespace BestService.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Models;
    using BestService.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditCompanyInputModel : IMapFrom<Company>, IMapTo<Company>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string OfficialSite { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        public string LogoImage { get; set; }

        public IFormFile LogoImageFile { get; set; }

        public string UserId { get; set; }
    }
}
