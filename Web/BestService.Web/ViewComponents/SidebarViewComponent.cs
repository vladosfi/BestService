﻿namespace BestService.Web.ViewComponents
{
    using BestService.Services.Data;
    using BestService.Web.ViewModels.SideBar;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "SideBar")]
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public SidebarViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new SideBarCategoriesViewModel
            {
                Categories = this.categoriesService.GetAll<SideBarCategoryViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
