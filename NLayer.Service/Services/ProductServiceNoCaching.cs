﻿using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using PointoFrameworks.StatusCodes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServiceNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseStatus<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productsDto=_mapper.Map<List<ProductWithCategoryDto>>(products);
            return PointoFrameworks.StatusCodes.Successful.OK<List<ProductWithCategoryDto>>.OKResponse(productsDto);
        }
    }
}
