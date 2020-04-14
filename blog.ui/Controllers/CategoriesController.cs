using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.data.context;
using blog.data.models;
using blog.business.repositories;

namespace blog.ui.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
        }

        public IActionResult Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                try
                {
                    _categoryRepository.Save();
                    return RedirectToAction("Index");
                    // return View("Index",_categoryRepository.GetAll()); 
                }
                catch
                {
                    return BadRequest();
                }
                // return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _categoryRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Update(category);
                    _categoryRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryRepository.Any(x => x.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            // var category = _categoryRepository.GetById(id);

            _categoryRepository.Delete(id);
            _categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // private bool CategoryExists(Guid id)
        // {
        //     return _context.Categories.Any(e => e.Id == id);
        // }
    }
}
