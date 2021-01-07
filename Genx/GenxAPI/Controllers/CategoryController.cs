using AutoMapper;
using GenxAPI.Model;
using GenxAPI.Model.Dtos;
using GenxAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenxAPI.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }


        /// <summary>
        /// Get list of Category.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoryDto>))]
        public IActionResult GetAllCategory()
        {
            var objList = categoryRepo.GetAllCategory();
            var objDto = new List<CategoryDto>();
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<CategoryDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual Category
        /// </summary>
        /// <param name="categoryId"> The Id of the category </param>
        /// <returns></returns>
        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetCategory(int categoryId)
        {
            var obj = categoryRepo.GetCategory(categoryId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<CategoryDto>(obj);
            return Ok(objDto);
        }

        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(201, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(ModelState);
            }
            if (categoryRepo.CategoryExists(categoryDto.CategoryName))
            {
                ModelState.AddModelError("", "Category Name Exists!");
                return StatusCode(404, ModelState);
            }
            var objCategory = mapper.Map<Category>(categoryDto);
            if (!categoryRepo.CreateCategory(objCategory))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {objCategory.CategoryName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCategory", new { categoryId = objCategory.CategoryId }, objCategory);
        }

        [HttpPatch("{categoryId:int}", Name = "UpdateCategory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null || categoryId != categoryDto.CategoryId)
            {
                return BadRequest(ModelState);
            }

            var objCategory = mapper.Map<Category>(categoryDto);
            if (!categoryRepo.UpdateCategory(objCategory))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {objCategory.CategoryName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!categoryRepo.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var objCategory = categoryRepo.GetCategory(categoryId);
            if (!categoryRepo.DeleteCategory(objCategory))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {objCategory.CategoryName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
