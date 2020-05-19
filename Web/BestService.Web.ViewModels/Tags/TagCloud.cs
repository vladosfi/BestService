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

        public int GetRankForTag(MenuTag tag)
        {
            if (this.CompaniesCount == 0)
            {
                return 1;
            }

            var result = (tag.Count * 100) / this.CompaniesCount;
            if (result <= 1)
            {
                return 1;
            }

            if (result <= 4)
            {
                return 2;
            }

            if (result <= 8)
            {
                return 3;
            }

            if (result <= 12)
            {
                return 4;
            }

            if (result <= 18)
            {
                return 5;
            }

            if (result <= 30)
            {
                return 6;
            }

            return result <= 50 ? 7 : 8;
        }
    }
}
