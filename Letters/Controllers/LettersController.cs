using Letters.Models;
using System.Net;
using System.Web.Mvc;

namespace Letters.Controllers
{
    public class LettersController : Controller
    {
        public ActionResult CreateLetter()
        {
            return View(new Letter());
        }

        [HttpPost]
        public ActionResult CreateLetter(Letter letter)
        {
            if (!ModelState.IsValid)
            {
                return View(letter);
            }

            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Add(letter);
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
            if (!ModelState.IsValid)
            {
                return View(letter);
            }

            using (LettersDbContext ctx = new LettersDbContext())
            {
                ctx.Letters.Find(letter.LetterId).Content = letter.Content;
                ctx.SaveChanges();
            }

            return RedirectToAction("AllLetters", "Home");
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

            return RedirectToAction("AllLetters", "Home");
        }
    }
}