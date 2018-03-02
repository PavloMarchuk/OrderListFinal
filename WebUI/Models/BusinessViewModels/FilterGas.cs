using DateLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models.BusinessViewModels
{
	[Serializable]
	public class FilterGas
    {
		[DisplayName("Фільтр по менеджеру")]
		public int ManagerIdFilter { get; set; }


		// параметри пейджингу
		[HiddenInput(DisplayValue = false)] public int PageIndex { get; set; }
		[HiddenInput(DisplayValue = false)]	public int PageSize { get; set; }
		[HiddenInput(DisplayValue = false)] public int TotalPages { get; set; }

		//параметри сортування	
		[HiddenInput(DisplayValue = false)] public string SortOrder { get; set; }
		[HiddenInput(DisplayValue = false)] public string NameSortParm { get; set; }
		[HiddenInput(DisplayValue = false)] public string DateSortParm { get; set; }

		

		// предикат фільтру(можна нескінченно дописувати)
		public Expression<Func<Invoice, bool>> Predicate()
		{
			var predicate = PredicateBuilder.New<Invoice>(true);
			//фільтр по менеджеру
			if (ManagerIdFilter > 0)
				predicate = predicate.And(p => p.ManagerId == ManagerIdFilter);

			return predicate;
		}

		public IOrderedQueryable<Invoice>  SortMethod(IQueryable<Invoice> l)
		{
			IOrderedQueryable<Invoice> res = l.OrderBy(s => s.Id); 

			//NameSortParm = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
			//DateSortParm = SortOrder == "Date" ? "date_desc" : "Date";

			//switch (SortOrder)
			//{
			//	case "name_desc":
			//		res = l.OrderByDescending(s => s.Id);						
			//		break;
			//	case "Date":
			//		res = l.OrderBy(s => s.Manager.LastName);
			//		break;
			//	case "date_desc":
			//		res = l.OrderByDescending(s => s.Manager.LastName);
			//		break;
			//	default:
			//		res = l.OrderBy(s => s.Id);
			//		break;
			//}

			return res;
		}
	}
}

