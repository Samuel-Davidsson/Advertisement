using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using AnnonsonMVC.ViewModels;
using Data.DataContext;
using Domain.Services;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleService _articelService;

        public ArticlesController(ArticleService articleService)
        {
            _articelService = articleService;
        }

        public IActionResult Index()
        {
            var articles = _articelService.GetAll().Where(t => t.UserId == 2);
            return View( articles.ToList());
        }

        public IActionResult Create()
        {
            //ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Name");
            //ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            //ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticelViewModel article)
        {
            //if (ModelState.IsValid)
            //{
                         
            //    var slug = article.Name.Replace(" ", "-").ToLower();
            //    article.Slug = slug;

            //    article.UserId = 2;
                
            //    _context.Add(article);
            //    await _context.SaveChangesAsync();

            //    var imageName = "aid" + article.ArticleId + "-" + Guid.NewGuid();
            //    article.ImageFileName = imageName;

            //    _context.Update(article);
            //    await _context.SaveChangesAsync();

               
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Company");
            //ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Category");
            return View();
        }
      }
    }

