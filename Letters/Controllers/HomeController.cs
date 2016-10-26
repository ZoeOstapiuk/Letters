using Letters.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Letters.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Letter letter)
        {
            if (letter == null)
            {
                letter = new Letter();
            }

            return View(letter);
        }

        [HttpPost]
        public ActionResult AddLetter(Letter letter)
        {
            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Add(letter);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult AllLetters()
        {
            IEnumerable<object> letters;
            using (LettersDbContext ctx = new LettersDbContext())
            {
                letters = ctx.Letters.ToList();
            }

            return View(letters);
        }

        [HttpPost]
        public ActionResult UpdateLetter(Letter letter)
        {
            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Find(letter.LetterId).Content = letter.Content;
                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters", new Letter());
        }
        
        public ActionResult DeleteLetter(int? id)
        {
            using (LettersDbContext ctx = new LettersDbContext())
            {
                var let = ctx.Letters.Find(id.Value);
                if (let != null)
                {
                    ctx.Letters.Remove(let);
                }

                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters", new Letter());
        }
    }
}