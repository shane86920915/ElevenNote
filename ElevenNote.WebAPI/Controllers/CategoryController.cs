using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());//taking the GetUserId and parsing it into a Guid and setting that equal to the variable userId.
            var categoryService = new CategoryService(userId); //Creating an instance of CategoryService to use those methods.
            return categoryService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var categoryService = CreateCategoryService();
            var category = categoryService.GetCategory();
            return Ok(category);
        }

        [HttpPost]
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var service = CreateCategoryService();


            if (!service.CreateCategory(category))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
