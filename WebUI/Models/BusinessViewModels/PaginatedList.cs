using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Models.BusinessViewModels
{
	public class PaginatedList<T> : List<T>
	{
		public FilterGas FilterInPL { get; private set; }
				
		//приватний конструктор (не може бути асинхронним)
		 PaginatedList(int count, FilterGas filter)
		{
			FilterInPL = filter;				
			
			this.AddRange(items);
		}		

		public static async Task<PaginatedList<T>> CreateAsync(IOrderedQueryable<T> source, FilterGas filter)
		{					
			//кількість елементів 
			int count = await source.CountAsync();
			filter.TotalPages = (int)Math.Ceiling(count / (double)filter.PageSize);
			//якщо поле "поточна сторінка" більше ніж кількість сторінок після зміни фільтру: 
			while (filter.PageIndex > filter.TotalPages)
			{
				filter.PageIndex--;
			}

			//вибірка потрібної сторінки
			items = await source.Skip((filter.PageIndex-1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
			
			return new PaginatedList<T>(count, filter);
		}
		
		private static List<T> items;
		public List<T> Items { get => items; /*set=> items = value;*/ }

		


		[HiddenInput(DisplayValue = false)]
		public bool HasPreviousPage
		{
			get
			{
				return (FilterInPL.PageIndex > 1);
			}
		}
		[HiddenInput(DisplayValue = false)]
		public bool HasNextPage
		{
			get
			{
				return (FilterInPL.PageIndex < FilterInPL.TotalPages);
			}
		}
	}
}
