using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Result;
using CardsServer.BLL.Services.Module;
using CardsServer.DAL.Repository;
using FluentAssertions;
using Moq;

namespace CardsServer.API.Tests
{
    public class ModuleServiceTests
    {
        private readonly Mock<IModuleRepository> _moduleRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IImageService> _imageServiceMock = new();
        private readonly Mock<IElementRepository> _elementRepositoryMock = new();
        private readonly Mock<IValidatorFactory> _validatorFactoryMock = new();
        private readonly Mock<IValidator> _validatorMock = new();

        private readonly ModuleService _moduleService;

        public ModuleServiceTests()
        {
            //_validatorFactoryMock.Setup(x => x.CreateValidator()).Returns(_validatorFactoryMock.Object);

            _moduleService = new ModuleService(
            _moduleRepositoryMock.Object,
            _userRepositoryMock.Object,
            _imageServiceMock.Object,
            _elementRepositoryMock.Object,
            _validatorFactoryMock.Object);
        }


        [Fact]
        public async Task EditModule_ReturnsFailure_WhenUserNotFound()
        {
            // Arrange
            int userId = 1;
            var moduleForEdit = new EditModule
            {
                Title = "Test",
                Description = "Test",
            };

            _userRepositoryMock
                .Setup(repo => repo.GetUser(x => x.Id == userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserEntity?)null);

            // Act
            Result result = await _moduleService.EditModule(moduleForEdit, userId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
        }
    }
}
