using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OData.WebAPI.Context;
using OData.WebAPI.Dtos;
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

            builder.EnableLowerCamelCase();
            builder.EntitySet<Category>("GetAllCategories");
            builder.EntitySet<Product>("GetAllProducts");
            builder.EntitySet<ProductDto>("GetAllProductsDto");
            builder.EntitySet<UserDto>("GetAllUsersDto");

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

        [HttpGet("GetAllUsersDto")]
        public IQueryable<UserDto> GetAllUsers()
        {

            var users = context.Users
                .Select(user => new UserDto {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    Address = user.Address,
                    Id = user.Id,
                    UserType = user.UserType,
                    UserTypeName = user.UserType.Name,
                    UserTypeValue = user.UserType.Value,
                })
                .AsQueryable();

            return users;

        }

    }

}