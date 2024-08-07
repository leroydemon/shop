﻿using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(ProductDto productDto)
        {
            var addedProduct = await _productRepository.AddAsync(_mapper.Map<Product>(productDto));

            return _mapper.Map<ProductDto>(addedProduct);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(item);
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(ProductFilter filter)
        {
            var spec = new ProductSpecification(filter);
            var products = await _productRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(ProductDto product)
        {
            var updatedProduct = await _productRepository.UpdateAsync(_mapper.Map<Product>(product));

            return _mapper.Map<ProductDto>(updatedProduct);
        }
    }
}
