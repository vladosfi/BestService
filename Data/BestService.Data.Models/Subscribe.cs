namespace BestService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BestService.Data.Common.Models;

    public class Subscribe : BaseDeletableModel<int>
    {
        [Required]
        public string Email { get; set; }

        public string Ip { get; set; }
    }
}
