namespace BestService.Web.ViewModels.Tags
{
    using System.Collections.Generic;

    public class TagCloud
    {
        public TagCloud()
        {
            this.MenuTags = new List<MenuTag>();
        }

        public int CompaniesCount { get; set; }

        public List<MenuTag> MenuTags { get; set; }
    }
}
