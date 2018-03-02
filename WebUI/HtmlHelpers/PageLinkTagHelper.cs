using DateLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using WebUI.Models.BusinessViewModels;

namespace WebUI.HtmlHelpers
{
	public class PageLinkTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;
		public PageLinkTagHelper(IUrlHelperFactory helperFactory)
		{
			urlHelperFactory = helperFactory;
		}
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PaginatedList<Invoice> PageModel { get; set; }
		public string PageAction { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			output.TagName = "div";

			// набор ссылок будет представлять список ul
			TagBuilder tag = new TagBuilder("ul");
			// застосовуємо Bootstrap pager
			tag.AddCssClass("pager");

			// формируем три ссылки - на текущую, предыдущую и следующую
			// создаем ссылку на предыдущую страницу 			
				TagBuilder prevItem = CreateTag(PageModel.FilterInPL.PageIndex - 1, urlHelper);
				tag.InnerHtml.AppendHtml(prevItem);
			
			// додаємо поточну сторінку
			TagBuilder currentItem = CreateTag(PageModel.FilterInPL.PageIndex, urlHelper);
			tag.InnerHtml.AppendHtml(currentItem);
			// создаем ссылку на следующую страницу 
			TagBuilder nextItem = CreateTag(PageModel.FilterInPL.PageIndex + 1, urlHelper);
			tag.InnerHtml.AppendHtml(nextItem);
			
			output.Content.AppendHtml(tag);
		}

		TagBuilder CreateTag(int nextPage, IUrlHelper urlHelper)
		{
			int currentPage = this.PageModel.FilterInPL.PageIndex;
			TagBuilder item = new TagBuilder("li");
			TagBuilder link = new TagBuilder("a");
			if (nextPage == currentPage)
			{
				link.MergeAttribute("disabled", "true");
				item.MergeAttribute("disabled", "true");
				// покажемо номер поточної сторінки візуально
				link.InnerHtml.Append(currentPage.ToString());
			}
			else 
			{
				// вписуємо номер сторінки в атрибут, якщо вона існує				
				if (nextPage < currentPage)
				{
					if (PageModel.HasPreviousPage)
					{
						item.MergeAttribute("nextPage", nextPage.ToString());
						item.AddCssClass("pagerli");
						link.InnerHtml.Append("Попередня");
					}						
					else					
						link.InnerHtml.Append("_________");
					
				}
				else
				{
					if (PageModel.HasNextPage)
					{
						item.MergeAttribute("nextPage", nextPage.ToString());
						item.AddCssClass("pagerli");
						link.InnerHtml.Append("Наступна");
					}
					else
						link.InnerHtml.Append("_________");

				}
			}
			
			item.InnerHtml.AppendHtml(link);
			return item;
		}
	}
}

