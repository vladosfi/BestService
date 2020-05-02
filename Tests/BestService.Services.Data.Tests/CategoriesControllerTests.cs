namespace BestService.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using BestService.Web.Controllers;
    using BestService.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class CategoriesControllerTests
    {
        [Fact]
        public void TestViewModelForGetAllForm()
        {
            // Create Mock services
            var mockServiceCategories = new Mock<ICategoriesService>();
            var mockServiceCompanies = new Mock<ICompaniesService>();

            mockServiceCategories.Setup(x => x.GetAll<CategoriesViewModel>(null))
                .Returns(new List<CategoriesViewModel>
                {
                    new CategoriesViewModel { Name = "CategoryName1" },
                    new CategoriesViewModel { Name = "CategoryName2" },
                });

            var controller = new CategoriesController(mockServiceCategories.Object, mockServiceCompanies.Object);
            var result = controller.All();
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<AllCategoriesViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as AllCategoriesViewModel;
            Assert.Equal(2, viewModel.Categories.Count());

            mockServiceCategories.Verify(x => x.GetAll<CategoriesViewModel>(null), Times.Once);
        }
    }
}
