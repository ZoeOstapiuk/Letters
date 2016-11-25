using Letters.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        [HttpPost]
        public ActionResult UpdateLetter(int id, int page)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Letter let = user.Letters.FirstOrDefault(l => l.LetterId == id);

            if (let == null)
            {
                return HttpNotFound();
            }

            return View(new LetterModel
            {
                LetterId = id,
                LetterPage = page,
                Letter = let.Content
            });
        }

        [HttpPost]
        public ActionResult UpdateLetterConfirmed(LetterModel content)
        {
            if (!ModelState.IsValid)
            {
                return View(content);
            }

            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Letter let = user.Letters.FirstOrDefault(l => l.LetterId == content.LetterId);

            if (let == null)
            {
                return HttpNotFound();
            }

            let.Content = content.Letter;
            UserManager.Update(user);

            return RedirectToAction("AllLetters", "Home", new { page = content.LetterPage });
        }
        
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
            string userId = String.Empty;
            using (ApplicationContext ctx = new ApplicationContext())
            {
                letter = ctx.Users.SelectMany(u => u.Letters).FirstOrDefault(u => u.LetterId == id.Value);
                email = letter.ApplicationUser.Email;
                userId = letter.ApplicationUserId;
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
                LetterId = letter.LetterId,
                AuthorId = userId
            });
        }
    }
}