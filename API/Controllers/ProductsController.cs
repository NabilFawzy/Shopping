using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using CoreEntities;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.DTOs;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        // private readonly IProductRepository _repo;

        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product>productRepo,
        IGenericRepository<ProductBrand>productBrandRepo, IGenericRepository<ProductType>productTypeRepo, IMapper mapper)
       {
            _mapper = mapper;
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task< ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(){
            
            var spec=new ProductsWithTypesAndBrandsSpecification();


            var products= await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }



       [HttpGet("{id}")]   
       [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task< ActionResult<ProductToReturnDto>> GetProduct(int id){

            var spec=new ProductsWithTypesAndBrandsSpecification(id);

            var product= await _productRepo.GetEntityWithSpec(spec);

            if(product==null)return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductToReturnDto>(product);
        }



        [HttpGet("brands")]
        public async Task< ActionResult<List<ProductBrand>>> GetGetProductBrands(){
            
            var productsBrands= await _productBrandRepo.ListAllAsync();
            return Ok(productsBrands);
        }


        [HttpGet("types")] 
        public async Task< ActionResult<List<ProductType>>> GetProductTypes(){
            
            var productsTypes= await _productTypeRepo.ListAllAsync();
            return Ok(productsTypes);
        }
    }
}