using Letters.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Letters.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = (i + 1).ToString();

                tag.AddCssClass("w3-btn-floating");
                tag.AddCssClass("paging-button");
                if (i + 1 == pageInfo.PageNumber)
                {
                    tag.AddCssClass("w3-yellow");
                }
                else
                {
                    tag.AddCssClass("w3-pale-yellow");
                }

                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
