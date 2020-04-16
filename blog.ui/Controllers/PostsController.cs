using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
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
            var posts = _postRepository.GetAll().AsQueryable().Include(x => x.PostCategories).ThenInclude(c => c.Category).ToList();

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
        public IActionResult Create([Bind("Title,Content,CreatedDate,FullName,Id")] Post post, Guid[] PostCategories)
        {
            if (ModelState.IsValid)
            {
                post.PostCategories = new List<PostCategory>();
                foreach (var item in PostCategories)
                {
                    post.PostCategories.Add(new PostCategory { CategoryId = item });
                }

                _postRepository.Add(post);
                _postRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name");
            return View(post);
        }


        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postRepository
                      .GetDefault(p => p.Id == id.Value)
                      .AsQueryable()
                      .Include(c => c.PostCategories)
                      .ThenInclude(c => c.Category);


            if (post == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name");

            ViewBag.CategoryIds = post.Select(c => c.PostCategories.Select(x =>
                x.Category.Id
          )).ToArray();

            return View(post.First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Content,CreatedDate,FullName,Id")] Post post, Guid[] PostCategories)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                { 
                    var ps = _postRepository
                              .GetDefault(p => p.Id == id)
                              .AsQueryable()
                              .Include(c => c.PostCategories)
                              .ThenInclude(c => c.Category).First();

                    ps.Title = post.Title;
                    ps.CreatedDate = post.CreatedDate;
                    ps.FullName = post.FullName;
                    ps.Content = post.Content;

                    ps.PostCategories.Clear();

                    foreach (var item in PostCategories)
                    {
                        ps.PostCategories.Add(new PostCategory { CategoryId = item });
                    }

                    _postRepository.Update(ps);
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
            ViewData["CategoryId"] = new SelectList(_categoryRepsitory.GetAll(), "Id", "Name");
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
