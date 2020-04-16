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


        public IActionResult Index()
        {
            return View();
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

    }
}