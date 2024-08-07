﻿using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;

namespace BussinessLogicLevel.Services
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDto> AddAsync(BrandDto brandDto)
        {
            var addedBrand = await _brandRepository.AddAsync(_mapper.Map<Brand>(brandDto));

            return _mapper.Map<BrandDto>(addedBrand);
        }

        public async Task<IEnumerable<BrandDto>> SearchAsync(BrandFilter filter)
        {
            var spec = new BrandSpecification(filter);
            var brand = await _brandRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<BrandDto>>(brand);
        }

        public async Task<BrandDto> GetByIdAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);

            return _mapper.Map<BrandDto>(brand);
        }

        public async Task RemoveAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            await _brandRepository.DeleteAsync(brand);
        }

        public async Task<BrandDto> UpdateAsync(BrandDto brand)
        {
            var updatedBrand = await _brandRepository.UpdateAsync(_mapper.Map<Brand>(brand));

            return _mapper.Map<BrandDto>(updatedBrand);
        }
    }
}
