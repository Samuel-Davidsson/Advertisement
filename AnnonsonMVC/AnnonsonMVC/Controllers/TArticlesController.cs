using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnnonsonMVC.Models;

namespace AnnonsonMVC.Controllers
{
    public class TArticlesController : Controller
    {
        private readonly annonsappenContext _context;

        public TArticlesController(annonsappenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var annonsappenContext = _context.TArticle.Where(t => t.UserId == 1).Include(t => t.Company);
            return View(await annonsappenContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.TCompany, "CompanyId", "Name");
            ViewData["CategoryId"] = new SelectList(_context.TCategory, "CategoryId", "Name");
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,CompanyId,UserId,Name,Slug,Description,ImagePath,ImageFileName,ImageFileFormat,ImageWidths,Price,PriceText,PriceUnit,PublishBegin,PublishEnd,IsDeleted,Modified,Created,Deleted,ImageUrl")] TArticle tArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.TCompany, "CompanyId", "Address", tArticle.CompanyId);
            return View(tArticle);
        }
      }
    }

