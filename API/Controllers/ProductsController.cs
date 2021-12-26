using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using CoreEntities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
     
        private readonly IProductRepository _repo;
        
        public ProductsController(IProductRepository repo){
            _repo = repo;
         
        }

        [HttpGet]
        public async Task< ActionResult<List<Product>>> GetProducts(){
            
            var products= await _repo.GetProductsAsync();
            return Ok(products);
        }



       [HttpGet("{id}")]   
        public async Task< ActionResult<Product>> GetProduct(int id){
            return await _repo.GetProductByIdAsync(id);
        }



        [HttpGet("brands")]
        public async Task< ActionResult<List<ProductBrand>>> GetGetProductBrands(){
            
            var productsBrands= await _repo.GetProductBrandsAsync();
            return Ok(productsBrands);
        }


        [HttpGet("types")] 
        public async Task< ActionResult<List<ProductType>>> GetProductTypes(){
            
            var productsTypes= await _repo.GetProductTypesAsync();
            return Ok(await _repo.GetProductTypesAsync());
        }
    }
}