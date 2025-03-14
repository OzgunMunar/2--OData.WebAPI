using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OData.WebAPI.Context;
using OData.WebAPI.Models;

namespace OData.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet("GetAllCategories")]
        public async Task<List<Category>> Get()
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }
    }
}

// 15:10