using MSF.Domain;
using System;

namespace MSF.Service
{
	public class ProductViewModel : Product
	{
		public string RowVersionString { get; set; }
	}

	public class ProductGetViewModel : ProductViewModel
	{
		public ProductGetViewModel()
		{
			RowVersionString = Convert.ToBase64String(RowVersion);
		}
		public string CategoryName { get; set; }
		public decimal ActualPrice
		{
			get
			{
				if (Discount.GetValueOrDefault() <= 0)
					return MRPPrice;

				if (IsDiscountInPercentage == true)
					return MRPPrice - (MRPPrice * (Discount.Value / 100));

				return MRPPrice - Discount.GetValueOrDefault();
			}
		}
		public string TaxName { get; set; }
		public string UOMName { get; set; }

	}
}
