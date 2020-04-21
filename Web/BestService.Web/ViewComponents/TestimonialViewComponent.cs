namespace BestService.Web.ViewComponents
{
    using BestService.Services.Data;
    using BestService.Web.ViewModels.Testimonial;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Testimonial")]
    public class TestimonialViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public TestimonialViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public IViewComponentResult Invoke(int count = 6)
        {
            var viewModel = new TestimonialsViewModel
            {
                Comments = this.commentsService.Get<TestimonialViewModel>(count),
            };

            return this.View(viewModel);
        }
    }
}
