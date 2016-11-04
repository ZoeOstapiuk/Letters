
using Letters.Models;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;

namespace Letters.Controllers
{
    public class LettersController : Controller
    {
        public ActionResult CreateLetterWithAuthor()
        {
            LetterAuthorView view = new LetterAuthorView
            {
                Letter = new Letter(),
                Author = new Author()
            };
            return View(view);
        }

        [HttpPost]
        public ActionResult CreateLetterWithAuthor(LetterAuthorView viewModel)
        {
            if (!ModelState.IsValid)
            {
                // If smth is wrong go back
                return View(viewModel);
            }

            if (viewModel.Author == null || viewModel.Letter == null)
            {
                return View("~/Views/Home/Index");
            }

            using (SantaDbContext ctx = new SantaDbContext())
            {
                viewModel.Author.Letters.Add(viewModel.Letter);
                viewModel.Letter.Author = viewModel.Author;

                // Author is added automatically
                ctx.Letters.Add(viewModel.Letter);
                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters", "Home");
        }

        public ActionResult EditLetter(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter;
            using (SantaDbContext ctx = new SantaDbContext())
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
            if (!ModelState.IsValid)
            {
                return View(letter);
            }

            using (SantaDbContext ctx = new SantaDbContext())
            {
                ctx.Letters.Find(letter.LetterId).Content = letter.Content;
                ctx.SaveChanges();
            }

            return RedirectToAction("AllAuthors", "Home");
        }

        public ActionResult DeleteLetter(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter;
            using (SantaDbContext ctx = new SantaDbContext())
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
            using (SantaDbContext ctx = new SantaDbContext())
            {
                ctx.Letters.Remove(ctx.Letters.Find(id));
                ctx.SaveChanges();
            }

            return RedirectToAction("AllAuthors", "Home");
        }
        
        public ActionResult ViewLetter(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter = null;
            Author author = null;
            using (SantaDbContext ctx = new SantaDbContext())
            {
                letter = ctx.Letters.Find(id.Value);
                author = letter.Author;
            }

            if (letter == null || author == null)
            {
                return HttpNotFound();
            }

            return PartialView("ReadonlyLetter", new LetterAuthorView { Letter = letter, Author = author });
        }
    }
}