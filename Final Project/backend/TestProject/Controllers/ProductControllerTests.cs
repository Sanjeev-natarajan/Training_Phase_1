using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingApplication.Controllers;
using ShoppingApplication.Models.DTOs;
using ShoppingApplication.Services;

namespace TestProject.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IProductService> _serviceMock;
        private Mock<IWebHostEnvironment> _envMock;
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IProductService>();
            _envMock = new Mock<IWebHostEnvironment>();

            _envMock.Setup(e => e.WebRootPath).Returns("wwwroot");

            _controller = new ProductsController(_serviceMock.Object, _envMock.Object);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.ControllerContext.HttpContext.Request.Scheme = "http";
            _controller.ControllerContext.HttpContext.Request.Host = new HostString("localhost");
        }

        [Test]
        public async Task GetAll_ReturnsOk_WithProducts()
        {
            var fakeProducts = new List<ProductDto>
            {
                new ProductDto { ProductId = 1, Name = "Apple", Price = 10 },
                new ProductDto { ProductId = 2, Name = "Orange", Price = 20 }
            };
            _serviceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(fakeProducts);

            var result = await _controller.GetAll() as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            var products = result.Value as IEnumerable<ProductDto>;
            Assert.That(products, Has.Exactly(2).Items);
        }

        [Test]
        public async Task GetById_ReturnsNotFound_WhenNotExists()
        {
            _serviceMock.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((ProductDto)null);

            var result = await _controller.GetById(99);

            Assert.That(result, Is.TypeOf<NotFoundResult>());

        }

        [Test]
        public async Task GetById_ReturnsOk_WhenExists()
        {
            var product = new ProductDto { ProductId = 1, Name = "Banana" };
            _serviceMock.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            var result = await _controller.GetById(1) as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(product));
        }

        [Test]
        public async Task SearchProducts_MapsImageUrlCorrectly()
        {
            var product = new ProductDto
            {
                ProductId = 1,
                Name = "Mango",
                ImageUrl = "/uploads/mango.jpg"
            };
            _serviceMock.Setup(s => s.SearchProductsAsync(null, null, null, null))
                        .ReturnsAsync(new List<ProductDto> { product });


            var actionResult = await _controller.SearchProducts(null, null, null, null);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var products = okResult.Value as List<ProductDto>;
            Assert.That(products[0].ImageUrl, Is.EqualTo("http://localhost/uploads/mango.jpg"));
        }

    }
}
