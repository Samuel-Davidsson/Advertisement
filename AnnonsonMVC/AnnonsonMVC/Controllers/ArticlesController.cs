using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnnonsonMVC.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace AnnonsonMVC.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly annonsappenContext _context;

        public ArticlesController(annonsappenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var annonsappenContext = _context.Article.Where(t => t.UserId == 2).Include(t => t.Company);
            return View(await annonsappenContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,CompanyId,UserId,Name,Slug,Description,ImagePath,ImageFileName,ImageFileFormat,ImageWidths,Price,PriceText,PriceUnit,PublishBegin,PublishEnd,IsDeleted,Modified,Created,Deleted,ImageUrl")] Article article, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                                
                article.ImageFileName = Convert.ToString(Guid.NewGuid());
                
                var slug = article.Name.Replace(" ", "-").ToLower();
                article.Slug = slug;


                article.UserId = 2;

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company.OrderBy(c =>c.Name), "CompanyId", "Company");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
            return View(article);
        }
      }
    }

