using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
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
        
    }
}