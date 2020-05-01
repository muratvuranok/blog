using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace blog.ui.Controllers
{

    using business.repositories;
    using Utility;
    using System;
    using Microsoft.AspNetCore.Http;
    using blog.data.models;

    public class PostImagesController : Controller
    {

        private readonly IPostRepository _postRepository;
        private readonly IPostImageRepository _postImageRepository;
        private readonly IFileUpload _fileUpload;

        public PostImagesController(IPostRepository postRepository, IPostImageRepository postImageRepository, IFileUpload fileUpload)
        {
            this._postRepository = postRepository;
            this._postImageRepository = postImageRepository;
            this._fileUpload = fileUpload;
        }


        public IActionResult Index(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Post");
            }
            return View(id);
        }

        [HttpGet]
        public IActionResult Upload(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Posts");
            }

            return View(id);
        }

        [HttpPost]
        public IActionResult Upload(IFormFile[] file, Guid? id)  // bilinçli olarak file bıraktım. dropzone içerisinde tanımlı kelime file,
        {
            if (id.HasValue)
            {
                Post post = _postRepository.GetById(id.Value);
                if (post == null)
                {
                    return NotFound();
                }

                if (file.Length > 0)
                {
                    foreach (var item in file)
                    {
                        var result = _fileUpload.Upload(item);
                        if (result.FileResult == FileResult.Succeded)
                        {
                            // PostImage image = new PostImage();
                            // image.ImageUrl = result.FileUrl;
                            // image.PostId = id.Value;

                            PostImage image = new PostImage
                            {
                                ImageUrl = result.FileUrl,
                                PostId = id.Value
                            };

                            _postImageRepository.Add(image);
                            _postImageRepository.Save();
                        }
                    }
                }
            }

            return View();
        }

        public IActionResult _Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var image = _postImageRepository.GetById(id.Value);
                if (image == null)
                {
                    return Json(HttpStatusCode.NoContent);
                }
                _postImageRepository.Delete(image);
                return Json(HttpStatusCode.OK);
            }
            return Json(HttpStatusCode.BadRequest);
        }

        public IActionResult _Active(Guid? id)
        {
            if (id.HasValue)
            {
                var image = _postImageRepository.GetById(id.Value);
                if (image == null)
                {
                    return Json(new { Code = HttpStatusCode.NoContent, Result = false });
                }

                var images = _postImageRepository.GetDefault(x => x.PostId == image.PostId).ToList();

                _postImageRepository.SetFalse(images);

                image.Active = true;
                _postImageRepository.Update(image);
                return Json(new { Code = HttpStatusCode.OK, Result = image.Active });
            }
            return Json(new { Code = HttpStatusCode.BadRequest, Result = false });
        }


        public IActionResult _Index(Guid? id)
        {
            if (id.HasValue)
            {
                var images = _postImageRepository.GetDefault(x => x.PostId == id.Value)
                .Select(x => new
                {
                    x.Id,
                    x.ImageUrl,
                    x.Active
                }).ToList();
                if (images.Count > 0)
                {
                    return Json(images);
                }
                else
                {
                    return Json(HttpStatusCode.NoContent);
                }
            }

            return Json(HttpStatusCode.BadRequest);
        }



    }
}