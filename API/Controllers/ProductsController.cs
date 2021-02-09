using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastucture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductsController:BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productsRepo
            ,IGenericRepository<ProductType> productTypeRepo
            ,IGenericRepository<ProductBrand> productBrandRepo
            ,IMapper mapper)
        {
            this.productsRepo = productsRepo;
            this.productTypeRepo = productTypeRepo;
            this.productBrandRepo = productBrandRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() {
            var spec =new ProductsWithTypesAndBrandsSpecification();
            var products = await productsRepo.ListAsync(spec);
            return Ok(mapper
                .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProducts(int id) {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product= await productsRepo.GetEntityWithSpec(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var products = await productBrandRepo.ListAllAsync();
            return Ok(products);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var products = await productTypeRepo.ListAllAsync();
            return Ok(products);
        }
    }
}
