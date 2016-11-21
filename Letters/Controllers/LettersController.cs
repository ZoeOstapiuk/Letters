
using Letters.Infrastructure;
using Letters.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Letters.Controllers
{
    public class LettersController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult CreateLetter()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateLetter(LetterModel content)
        {
            if (!ModelState.IsValid)
            {
                // If smth is wrong go back
                return View(content);
            }

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            user.Letters.Add(new Letter { Content = content.Letter });
            await UserManager.UpdateAsync(user);
            
            return RedirectToAction("AllLetters", "Home");
        }
/*
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
        */
        public ActionResult ReadLetter(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Letter letter = new Letter();
            string email = String.Empty;
            using (ApplicationContext ctx = new ApplicationContext())
            {
                letter = ctx.Users.SelectMany(u => u.Letters).FirstOrDefault(u => u.LetterId == id.Value);
                email = letter.Author.Email;
            }

            if (letter == null)
            {
                return HttpNotFound();
            }

            return PartialView("ReadLetter", new LetterAuthorModel { Letter = letter.Content, Email = email });
        }
    }
}