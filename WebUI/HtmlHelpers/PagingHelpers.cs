using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(
            this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            //for (int i = 1; i <= pagingInfo.TotalPages; i++)
            //{
            //    TagBuilder tag = new TagBuilder("a");
            //    tag.MergeAttribute("href", pageUrl(i));
            //    tag.InnerHtml = i.ToString();
            //    if (i == pagingInfo.CurrentPage)
            //        tag.AddCssClass("selected");
            //    result.Append(tag.ToString());
            //}

            TagBuilder tag = new TagBuilder("a");

            if (pagingInfo.CurrentPage != 1)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage - 1));
                tag.InnerHtml = "Previous page";
                result.Append(tag.ToString());
            }

            tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage));
            tag.InnerHtml = pagingInfo.CurrentPage.ToString();
            tag.AddCssClass("selected");
            result.Append(tag.ToString());

            if (pagingInfo.CurrentPage != pagingInfo.TotalPages)
            {
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(pagingInfo.CurrentPage + 1));
                tag.InnerHtml = "Next page";
                result.Append(tag.ToString());
            }
           
            return MvcHtmlString.Create(result.ToString());
        }
    }
}