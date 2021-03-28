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
        private CategoryService _categoryService = new CategoryService();



        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            if (!_categoryService.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var categories = _categoryService.GetCategories();
            return Ok(categories);
        }

        public IHttpActionResult Get(int id)
        {
            var categories = _categoryService.GetCategoryById(id);
            return Ok (categories);
        }

        [HttpPut]
        public IHttpActionResult Update()
        {

        }


    }
}
