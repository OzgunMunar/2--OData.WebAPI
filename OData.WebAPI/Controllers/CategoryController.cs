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
    public class CategoryController(ApplicationDbContext context) : ODataController
    {
        public static IEdmModel GetEdmModel( )
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<Category>("GetAllCategories");

            return builder.GetEdmModel();
        }
        [HttpGet("GetAllCategories")]
        [EnableQuery]
        public IQueryable<Category> Get()
        {
            var categories = context.Categories.AsQueryable();
            return categories;
        }
        
    }
}