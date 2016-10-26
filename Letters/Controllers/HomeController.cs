using Letters.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Letters.Controllers
{
    public class HomeController : Controller
    {
        private const int ITEMS_PER_PAGE = 8;

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CreateLetter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateLetter(Letter letter)
        {
            // TODO: check ModelState
            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Add(letter);
                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters");
        }

        public ActionResult AllLetters(int page = 1)
        {
            IEnumerable<Letter> letters;
            using (LettersDbContext ctx = new LettersDbContext())
            {
                letters = ctx.Letters.OrderBy(l => l.LetterId).Skip((page - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
            }

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = ITEMS_PER_PAGE, TotalItems = letters.Count() };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Letters = letters };
            return View(ivm);
        }
        
        public ActionResult EditLetter(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter;
            using (LettersDbContext ctx = new LettersDbContext())
            {
                letter = ctx.Letters.Find(id.Value);
            }

            if (letter == null)
            {
                return HttpNotFound();
            }

            return View("EditView", letter);
        }
        
        [HttpPost]
        public ActionResult EditLetter(Letter letter)
        {
            // TODO: check ModelState
            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Find(letter.LetterId).Content = letter.Content;
                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters");
        }

        public ActionResult DeleteLetter(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter;
            using (LettersDbContext ctx = new LettersDbContext())
            {
                letter = ctx.Letters.Find(id.Value);
            }

            if (letter == null)
            {
                return HttpNotFound();
            }

            return View("DeleteLetter", letter);
        }

        [HttpPost]
        public ActionResult DeleteLetter(int id)
        {
            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Remove(ctx.Letters.Find(id));
                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters");
        }
    }
}