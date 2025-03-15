using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Odata.WebAPI.Dtos;
using OData.WebAPI.Context;
using OData.WebAPI.Models;

namespace OData.WebAPI.Controllers
{
    
    [Route("odata")]
    [ApiController]
    [EnableQuery]
    public class ODataGetMethodsController(ApplicationDbContext context) : ODataController
    {
        public static IEdmModel GetEdmModel( )
        {

            ODataConventionModelBuilder builder = new();

            builder.EntitySet<Category>("GetAllCategories");
            builder.EntitySet<Product>("GetAllProducts");
            builder.EntitySet<ProductDto>("GetAllProductsDto");

            return builder.GetEdmModel();

        }

        [HttpGet("GetAllCategories")]
        public IQueryable<Category> GetAllCategories()
        {
            var categories = context.Categories.AsQueryable();
            return categories;
        }

        [HttpGet("GetAllProducts")]
        public IQueryable<Product> GetAllProducts()
        {
            var products = context.Products.AsQueryable();
            return products;
        }
        
        [HttpGet("GetAllProductsDto")]
        public IQueryable<ProductDto> GetAllProductsDto()
        {

            var products = context.Products.Select(product => new ProductDto
            {
                
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category != null ? product.Category.Name : ""

            })
            .AsQueryable();

            return products;

        }

    }

}