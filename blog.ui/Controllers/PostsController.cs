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
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepsitory;

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            this._postRepository = postRepository;
            this._categoryRepsitory = categoryRepository;
        }

        // GET: Posts
        public IActionResult Index()
        {
            var posts = _postRepository.GetAll().AsQueryable().Include(p => p.Category);
            return View(posts);
        }


        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }


        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content,CreatedDate,FullName,CategoryId,Id")] Post post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Add(post);
                _postRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name", post.CategoryId);
            return View(post);
        }


        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name", post.CategoryId);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Content,CreatedDate,FullName,CategoryId,Id")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _postRepository.Update(post);
                    _postRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_postRepository.Any(x => x.Id == id))
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
            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name", post.CategoryId);
            return View(post);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository.GetById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            // var post = _postRepository.GetById(id);
            _postRepository.Delete(id);
            _postRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
