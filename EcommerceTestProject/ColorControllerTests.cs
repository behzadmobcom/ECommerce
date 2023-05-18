using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ECommerce.API.Controllers;
using ECommerce.API.Interface;
using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Microsoft.Extensions.Logging;

namespace EcommerceTestProject
{
    [TestFixture]
    public class ColorControllerTests
    {
        private ColorsController _controller;
        private Mock<IColorRepository> _repository;
        private Mock<ILogger<ColorsController>> _logger;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IColorRepository>();
            _logger = new Mock<ILogger<ColorsController>>();
            _controller = new ColorsController(_repository.Object, _logger.Object);
        }

        [Test]
        public async Task GetById_ReturnsSuccess_WhenIdExists()
        {
            // Arrange
            var id = 1;
            var cancellationToken = new CancellationToken();
            var color = new Color { Id = id, Name = "Red" };
            _repository.Setup(x => x.GetByIdAsync(cancellationToken, id)).ReturnsAsync(color);

            // Act
            var result = await _controller.GetById(id, cancellationToken);
            var _colorResult = result.Value;

            // Assert
            var okResult = result.Result as OkObjectResult;
            var apiResult = okResult.Value as ApiResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(apiResult.Code, Is.EqualTo(ResultCode.Success));
            Assert.That(apiResult.ReturnData, Is.EqualTo(color));

        }

        [Test]
        public async Task GetById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var id = 1;
            var cancellationToken = new CancellationToken();
            _repository.Setup(x => x.GetByIdAsync(cancellationToken, id)).ReturnsAsync((Color)null);

            // Act
            var result = await _controller.GetById(id, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var apiResult = okResult.Value as ApiResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(apiResult.Code, Is.EqualTo(ResultCode.NotFound));
        }

        [Test]
        public async Task GetById_ReturnsDatabaseError_WhenExceptionIsThrown()
        {
            // Arrange
            var id = 1;
            var cancellationToken = new CancellationToken();
            _repository.Setup(x => x.GetByIdAsync(cancellationToken, id)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetById(id, cancellationToken);

            // Assert
            var okResult = result.Result as OkObjectResult;
            var apiResult = okResult.Value as ApiResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(apiResult.Code, Is.EqualTo(ResultCode.DatabaseError));
            Assert.That(apiResult.Messages, Contains.Item("اشکال در سمت سرور"));
        }
    }
}