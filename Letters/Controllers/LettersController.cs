
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
                return View(content);
            }

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            Debug.WriteLine(user == null);
            user.Letters.Add(new Letter
            {
                Content = content.Letter,
                DateTimeCreation = DateTime.Now
            });
            await UserManager.UpdateAsync(user);

            TempData["one-day-warning"] = "You have one day to cancel the letter";
            return RedirectToAction("AllLetters", "Home");
        }
/*
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
        */
        [HttpPost]
        public async Task<ActionResult> DeleteLetter(int id, int page)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            Letter letter = user.Letters
                                .Where(l => l.LetterId == id)
                                .FirstOrDefault();
            if (letter == null)
            {
                return HttpNotFound();
            }
            
            user.Letters.Remove(letter);
            await UserManager.UpdateAsync(user);

            return RedirectToAction("AllLetters", "Home", new { page = page });
        }
        
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
                email = letter.ApplicationUser.Email;
            }

            if (letter == null)
            {
                return HttpNotFound();
            }

            return PartialView("ReadLetter", new LetterAuthorModel
            {
                Letter = letter.Content,
                Email = email,
                Date = letter.DateTimeCreation,
                LetterId = letter.LetterId
            });
        }
    }
}