namespace BestService.Web.ViewModels.Companies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddCompanyInputModel
    {
        [Required]
        [Range(2, 50)]
        public string Name { get; set; }

        [Required]
        [Range(20, 3000)]
        public string Description { get; set; }

        public string Image { get; set; }

        public string OfficialSite { get; set; }

        //public int CategoryId { get; set; }

        //public virtual Category Category { get; set; }
    }
}
