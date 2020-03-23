namespace BestService.Web.ViewModels.Home
{
    public class IndexCategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url => $"/c/{this.Name.Replace(' ', '-')}";
    }
}