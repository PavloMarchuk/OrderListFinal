using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DateLayer.Models
{
	public partial class Invoice
	{
		public int Id { get; set; }


		[Required(ErrorMessage = "Номер накладної не вказано")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Номер накладної повинен бути від 5 до 50 символів")]
		[DisplayName("Номер накладної")]
		public string InvoiceNumber { get; set; }

		[Required(ErrorMessage = "Дата накладної відсутня")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd' 'MMMM' 'yyyy }", ApplyFormatInEditMode = true, NullDisplayText = "Дата накладної відсутня")]
		[DisplayName("Дата накладної")]
		public DateTime DateCreated { get; set; }

		[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]		
		[DisplayName("Відвантажено")]
		public DateTime? DateOfShipment { get; set; }

		[Required(ErrorMessage = "Не вибрано менеджера")]
		[Range(1, Int32.MaxValue, ErrorMessage = "Не вибрано менеджера")]
		[DisplayName("Менеджер")]
		public int ManagerId { get; set; }

		[DisplayName("Зауваження")]
		public string Annotation { get; set; }


		[DisplayName("Менеджер")]
		[StringLength(512, ErrorMessage = "Занадто довгий текст")]
		public Manager Manager { get; set; }
    }
}
