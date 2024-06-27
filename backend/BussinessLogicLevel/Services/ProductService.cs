﻿using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel;
using DbLevel.Models;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Services
{
    public class ProductService : IProductService
    {
        private readonly Repository<Product> _productRepository;
        private readonly IMapper _mapper;
        public ProductService(Repository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var addedProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(addedProduct);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(item);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(product);
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);
        }
    }
}