using Letters.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Letters.Controllers
{
    public class HomeController : Controller
    {
        private const int ITEMS_PER_PAGE = 6;

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AllLetters(int page = 1)
        {
            IEnumerable<Letter> letters;
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = ITEMS_PER_PAGE };
            using (ApplicationContext ctx = new ApplicationContext())
            {
                letters = ctx.Users
                    .SelectMany(u => u.Letters)
                    .OrderBy(l => l.LetterId)
                    .Skip((page - 1) * ITEMS_PER_PAGE)
                    .Take(ITEMS_PER_PAGE).ToList();

                pageInfo.TotalItems = letters.Count();
            }

            PaginationViewModel ivm = new PaginationViewModel
            {
                PageInfo = pageInfo,
                Letters = letters.ToList()
            };
            return View(ivm);
        }
    }
}