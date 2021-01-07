using Genx.Web.Models;
using Genx.Web.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genx.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepo;

        public CategoryController(ICategoryRepository npRepo)
        {
            this.categoryRepo = npRepo;
        }

        public async Task<IActionResult> Index()
        {
            //return View(new Category() { });
            return View(await categoryRepo.GetAllAsync(SD.CategoryAPIPath));
        }
        //public async Task<IEnumerable<Category>> GetAllCategory()
        //{
        //    return v await categoryRepo.GetAllAsync(SD.CategoryAPIPath);
        //}

        public async Task<IActionResult> Upsert(int? id)
        {
            Category obj = new Category();

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await categoryRepo.GetAsync(SD.CategoryAPIPath, id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {               
                if (obj.CategoryId == 0)
                {
                    await categoryRepo.CreateAsync(SD.CategoryAPIPath, obj);
                }
                else
                {
                    await categoryRepo.UpdateAsync(SD.CategoryAPIPath + obj.CategoryId, obj);
                }
                return RedirectToAction(nameof(Index));               
            }
            else
            {
                return View(obj);
            }
        }
    }
}