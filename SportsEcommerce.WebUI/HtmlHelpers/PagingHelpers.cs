using SportsEcommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        /// <summary>
        /// Generates the HTML for a set of page links using the information prodvided in <paramref name="pagingInfo"/> Object
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pagingInfo"></param>
        /// <param name="pageUrl">a delegate used to geenrate links to view other pages</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            // allows for concatenating multiple strings
            StringBuilder result = new StringBuilder();

            for (int i = 1; i < pagingInfo.TotalPages; i++)
            {
                // set the html properties
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");

                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}