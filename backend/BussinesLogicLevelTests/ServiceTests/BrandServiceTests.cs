using AutoMapper;
using BussinessLogicLevel.Services;
using DbLevel.Interfaces;
using DbLevel.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class BrandServiceTests
    {
        private readonly Mock<IRepository<Brand>> _mockBrandRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BrandService _brandService;

        public BrandServiceTests()
        {
            _mockBrandRepository = new Mock<IRepository<Brand>>();
            _mockMapper = new Mock<IMapper>();

            _brandService = new BrandService(
                _mockBrandRepository.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedBrandDto()
        {
            // Arrange
            var brandDto = new BrandDto { Name = "Brand1" };
            var brand = new Brand { Id = Guid.NewGuid(), Name = brandDto.Name };

            _mockMapper.Setup(x => x.Map<Brand>(brandDto)).Returns(brand);
            _mockBrandRepository.Setup(x => x.AddAsync(brand)).ReturnsAsync(brand);
            _mockMapper.Setup(x => x.Map<BrandDto>(brand)).Returns(brandDto);

            // Act
            var result = await _brandService.AddAsync(brandDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(brandDto);
            _mockBrandRepository.Verify(r => r.AddAsync(brand), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBrandDtos()
        {
            // Arrange
            var brands = new List<Brand>
            {
                new Brand { Id = Guid.NewGuid(), Name = "Brand 1" },
                new Brand { Id = Guid.NewGuid(), Name = "Brand 2" }
            };

            var brandDtos = brands.Select(b => new BrandDto { Name = b.Name }).ToList();

            _mockBrandRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(brands);
            _mockMapper.Setup(m => m.Map<IEnumerable<BrandDto>>(brands)).Returns(brandDtos);

            // Act
            var result = await _brandService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(brandDtos, options => options.ComparingByMembers<BrandDto>());
            _mockBrandRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectBrand()
        {
            // Arrange
            var id = Guid.NewGuid();
            var brand = new Brand { Name = "Test Brand" };
            var brandDto = new BrandDto { Name = brand.Name };

            _mockBrandRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(brand);
            _mockMapper.Setup(x => x.Map<BrandDto>(brand)).Returns(brandDto);

            // Act
            var result = await _brandService.GetByIdAsync(id);

            // Assert
            result.Name.Should().Be(brand.Name);
            _mockBrandRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveBrand()
        {
            // Arrange
            var id = Guid.NewGuid();
            var brand = new Brand { Id = id, Name = "Test Brand" };

            _mockBrandRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(brand);
            _mockBrandRepository.Setup(r => r.DeleteAsync(brand)).Returns(Task.CompletedTask);

            // Act
            await _brandService.RemoveAsync(id);

            // Assert
            _mockBrandRepository.Verify(r => r.DeleteAsync(brand), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBrand()
        {
            // Arrange
            var brandDto = new BrandDto { Name = "Updated Brand Name" };
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Original Brand Name" };
            var mappedBrand = new Brand { Id = brand.Id, Name = brandDto.Name };

            _mockMapper.Setup(m => m.Map<Brand>(brandDto)).Returns(mappedBrand);
            _mockBrandRepository.Setup(r => r.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(brand);

            // Act
            await _brandService.UpdateAsync(brandDto);

            // Assert
            _mockBrandRepository.Verify(r => r.UpdateAsync(It.Is<Brand>(b => b.Id == mappedBrand.Id && b.Name == mappedBrand.Name)), Times.Once);
        }
    }
}


