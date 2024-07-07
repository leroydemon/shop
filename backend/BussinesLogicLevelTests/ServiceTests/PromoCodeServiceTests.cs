using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class PromoCodeServiceTests
    {
        private readonly Mock<IRepository<PromoCode>> _mockPromoCodeRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PromoCodeService _promoCodeService;
        public PromoCodeServiceTests()
        {
            _mockPromoCodeRepository = new Mock<IRepository<PromoCode>>();
            _mockMapper = new Mock<IMapper>();
            _promoCodeService = new PromoCodeService(_mockPromoCodeRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedPromoCodeDto()
        {
            // Arrange
            var promoCodeDto = new PromoCodeDto { Code = "TESTCODE", AmountDiscoint = 10, ExpireDate = DateTime.UtcNow.AddDays(30), IsActive = true };
            var promoCode = new PromoCode { Code = "TESTCODE", AmountDiscoint = 10, ExpireDate = DateTime.UtcNow.AddDays(30), IsActive = true };
            var addedPromoCode = new PromoCode { Id = Guid.NewGuid(), Code = "TESTCODE", AmountDiscoint = 10, ExpireDate = DateTime.UtcNow.AddDays(30), IsActive = true };

            _mockMapper.Setup(m => m.Map<PromoCode>(promoCodeDto)).Returns(promoCode);
            _mockPromoCodeRepository.Setup(r => r.AddAsync(promoCode)).ReturnsAsync(addedPromoCode);
            _mockMapper.Setup(m => m.Map<PromoCodeDto>(addedPromoCode)).Returns(promoCodeDto);

            // Act
            var result = await _promoCodeService.AddAsync(promoCodeDto);

            // Assert
            result.Should().BeEquivalentTo(promoCodeDto);
            _mockPromoCodeRepository.Verify(r => r.AddAsync(promoCode), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var promoCodeId = Guid.NewGuid();
            var promoCode = new PromoCode { Id = promoCodeId, Code = "TESTCODE", AmountDiscoint = 10, ExpireDate = DateTime.UtcNow.AddDays(30), IsActive = true };

            _mockPromoCodeRepository.Setup(r => r.GetByIdAsync(promoCodeId)).ReturnsAsync(promoCode);
            _mockPromoCodeRepository.Setup(r => r.DeleteAsync(promoCode)).Returns(Task.CompletedTask);

            // Act
            await _promoCodeService.DeleteAsync(promoCodeId);

            // Assert
            _mockPromoCodeRepository.Verify(r => r.GetByIdAsync(promoCodeId), Times.Once);
            _mockPromoCodeRepository.Verify(r => r.DeleteAsync(promoCode), Times.Once);
        }
        [Fact]
        public async Task FindByIdAsync_ShouldReturnPromoCodeDto()
        {
            // Arrange
            var promoCodeId = Guid.NewGuid();
            var promoCode = new PromoCode { Id = promoCodeId, Code = "TESTCODE", AmountDiscoint = 10, ExpireDate = DateTime.UtcNow.AddDays(30), IsActive = true };
            var promoCodeDto = new PromoCodeDto { Code = "TESTCODE", AmountDiscoint = 10, ExpireDate = DateTime.UtcNow.AddDays(30), IsActive = true };

            _mockPromoCodeRepository.Setup(r => r.GetByIdAsync(promoCodeId)).ReturnsAsync(promoCode);
            _mockMapper.Setup(m => m.Map<PromoCodeDto>(promoCode)).Returns(promoCodeDto);

            // Act
            var result = await _promoCodeService.FindByIdAsync(promoCodeId);

            // Assert
            result.Should().BeEquivalentTo(promoCodeDto);
            _mockPromoCodeRepository.Verify(r => r.GetByIdAsync(promoCodeId), Times.Once);
        }
       
        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var promoCodeDto = new PromoCodeDto { Code = "UPDATEDCODE", AmountDiscoint = 20, ExpireDate = DateTime.UtcNow.AddDays(90), IsActive = false };
            var promoCode = new PromoCode { Id = Guid.NewGuid(), Code = "UPDATEDCODE", AmountDiscoint = 20, ExpireDate = DateTime.UtcNow.AddDays(90), IsActive = false };

            _mockMapper.Setup(m => m.Map<PromoCode>(promoCodeDto)).Returns(promoCode);
            _mockPromoCodeRepository.Setup(r => r.UpdateAsync(promoCode)).ReturnsAsync(promoCode);

            // Act
            await _promoCodeService.UpdateAsync(promoCodeDto);

            // Assert
            _mockPromoCodeRepository.Verify(r => r.UpdateAsync(promoCode), Times.Once);
        }
    }
}
