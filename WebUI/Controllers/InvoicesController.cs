using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DateLayer.Models;
using EFCoreGenericRepository.Common;
using WebUI.Models.BusinessViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
	[Authorize]
	public class InvoicesController : Controller
	{
		private readonly KyivgazDBContext _context;
		IGenericRepository<Invoice> invoiceRep;
		IGenericRepository<Manager> мanagerRep;
		public InvoicesController(KyivgazDBContext context, IGenericRepository<Invoice> invoiceRep, IGenericRepository<Manager> мanagerRep)
		{
			_context = context;
			this.invoiceRep = invoiceRep;
			this.мanagerRep = мanagerRep;
		}

		[HttpGet]
		public IActionResult Filter()
		{
			// зброс сторінок
			FilterGas filter = new FilterGas { PageIndex = 1, PageSize = 5};
			ViewData["ManagerIdFilter"] = new SelectList(GetEmpty.Union(_context.Manager), "Id", "LastName");
			return View(filter);
		}

		public async Task<IActionResult> ListFilter()
		{		
			FilterGas filter = null;
			if (HttpContext.Session.Keys.Contains("filter"))
			{
				filter = HttpContext.Session.Get<FilterGas>("filter");
				if (filter.PageIndex == 0) filter.PageIndex = 1;
			}

			IQueryable<Invoice> filtred = invoiceRep.FindBy(filter.Predicate()).Include(i => i.Manager).AsNoTracking();
			IOrderedQueryable<Invoice> sorted = filter.SortMethod(filtred);

			PaginatedList<Invoice> pList = await PaginatedList<Invoice>.CreateAsync(sorted, filter);	

			return View(pList);
		}




		public async Task<IActionResult> Edit(int? id)
		{
			ViewData["ManagerIdEditor"] = new SelectList(GetEmpty.Union(_context.Manager), "Id", "LastName");

			//якщо не вказано ID, створимо нову накладну
			if (id == null | id == 0)
			{

				ViewData["EditMessage"] = "Створення нової накладної";
				//максимальний номер накладної		
				int num = _context.Invoice.Max(Inv => Inv.Id);
				num++;
				Invoice model = new Invoice
				{
					DateCreated = DateTime.Today
					,
					InvoiceNumber = $"ТТН-{num.ToString("D4")}"
				};

				return View(model);
			}

			// як що редагуємо стару
			var invoice = await _context.Invoice.SingleOrDefaultAsync(m => m.Id == id);
			if (invoice == null)
			{
				return NotFound();
			}
			ViewData["EditMessage"] = $"Редагування накладної № {invoice.InvoiceNumber}";
			return View(invoice);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNumber,DateCreated,DateOfShipment,ManagerId,Annotation")] Invoice model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var ModelInvoiceId = model.Id;
					if (model.Id == 0)
					{
						await invoiceRep.AddAsyn(model);
					}
					else
					{
						/*await*/
						invoiceRep.Update(model, model.Id);
					}
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!InvoiceExists(model.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Filter));
			}
			ViewData["ManagerId"] = new SelectList(_context.Manager, "Id", "LastName", model.ManagerId);
			return View(model);
		}


		[HttpPost]
		public IActionResult Filter(FilterGas model)
		{
			HttpContext.Session.Set<FilterGas>("filter", model);
			return Json("OK");
		}
		IEnumerable<Manager> GetEmpty
		{
			get
			{
				yield return new Manager { Id = 0, LastName = "Виберіть менеджера" };
			}
		}
		private bool InvoiceExists(int id)
		{
			return _context.Invoice.Any(e => e.Id == id);
		}
	}
}

