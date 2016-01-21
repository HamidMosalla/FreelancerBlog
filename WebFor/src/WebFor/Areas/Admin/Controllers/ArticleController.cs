using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using WebFor.Models;
using WebFor.Repositories;

namespace WebFor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private IUnitOfWork _uw;
        private IHostingEnvironment _environment;

        public ArticleController(IUnitOfWork uw, IHostingEnvironment environment)
        {
            _uw = uw;
            _environment = environment;
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Article article)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }


        public ActionResult TagLookup(string term)
        {
            var tags = new List<string>();

            foreach (var item in _uw.ArticleTagRepository.GetAll())
            {
                tags.Add(item.ArticleTagName);
            }
            //var results = tags.Where(n => n.ToLower().Contains(term.ToLower()));
            return Json(tags.ToArray());
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor,
           string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;

            try
            {
                if (upload != null && upload.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(ContentDispositionHeaderValue.Parse(upload.ContentDisposition).FileName.Trim('"'));
                    var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(upload.ContentDisposition).FileName.Trim('"'));

                    var vFileName = fileName + " - " + DateTime.Now.ToString("yyyyMMdd-HHMMssff") + extension;
                    var vFolderPath = Path.Combine(_environment.WebRootPath, "Files", "ArticleUploads");

                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }

                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    await upload.SaveAsAsync(vFilePath);
                    vImagePath = Url.Content("/Files/ArticleUploads/" + vFileName);
                    vMessage = "The file uploaded successfully.";
                }
            }
            catch (Exception e)
            {
                vMessage = "There was an issue uploading:" + e.Message;
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput, "text/html");
        }
    }
}
