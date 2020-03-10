using MSF.Domain;
using System;

namespace MSF.Service
{
	public class ProductViewModel
	{
		public long ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductCode { get; set; }

		public string ProductImage { get; set; }

		public string DisplayName { get; set; }

		public decimal MRPPrice { get; set; }

		public decimal? Discount { get; set; }

		public bool? IsDiscountInPercentage { get; set; }

        public int CategoryId { get; set; }

		public int MinimumQuantity { get; set; }

		public int TaxId { get; set; }

		public int UOMId { get; set; }

		public int? BulkUOMId { get; set; }

		public int? BulkUOMQty { get; set; }

		public int OpeningStock { get; set; }

		public string RowVersion { get; set; }

		public static implicit operator Product(ProductViewModel viewModel)
		{
			return new Product
			{
				ID = viewModel.ProductId,
				ProductCode = viewModel.ProductCode,
				ProductImage = string.IsNullOrEmpty( viewModel.ProductImage)? null: Convert.FromBase64String(viewModel.ProductImage),
				ProductName = viewModel.ProductName,
				MRPPrice = viewModel.MRPPrice,
				DisplayName = viewModel.DisplayName,
				Discount = viewModel.Discount,
				RowVersion = string.IsNullOrEmpty(viewModel.RowVersion) ? null :Convert.FromBase64String(viewModel.RowVersion),
				CategoryId = viewModel.CategoryId,
				MinimumQuantity = viewModel.MinimumQuantity,
				UOMId = viewModel.UOMId,
				TaxId = viewModel.TaxId,
				BulkUOMId = viewModel.BulkUOMId,
				BulkUOMQty = viewModel.BulkUOMQty,
				IsDiscountInPercentage = viewModel.IsDiscountInPercentage,
				OpeningStock = viewModel.OpeningStock
			};
		}
	}

	public class ProductGetViewModel : ProductViewModel
	{
		public string CategoryName { get; set; }
		public decimal ActualPrice { get; set; }
		public string TaxName { get; set; }
		public string UOMName { get; set; }

		public static implicit operator ProductGetViewModel(Product product)
		{
			
			return new ProductGetViewModel
			{
				ProductId = product.ID,
				ProductCode = product.ProductCode,
				ProductImage = (product.ProductImage == null) ? string.Empty : Convert.ToBase64String(product.ProductImage),
				ProductName = product.ProductName,
				MRPPrice = product.MRPPrice,
                IsDiscountInPercentage = product.IsDiscountInPercentage.GetValueOrDefault(),
				DisplayName = product.DisplayName,
				Discount = product.Discount,
				RowVersion = Convert.ToBase64String(product.RowVersion),
				CategoryId = product.CategoryId,
				MinimumQuantity = product.MinimumQuantity,
				UOMId = product.UOMId,
				TaxId = product.TaxId,
				OpeningStock = product.OpeningStock,
				CategoryName = product.Category?.CategoryName,
				UOMName = product.UOM?.UOMAbbr,
				TaxName = product.Tax?.TaxName,
				ActualPrice = getActualPrice(product.MRPPrice, product.Discount, product.IsDiscountInPercentage)
			};

		}

		private static decimal getActualPrice(decimal mrpPrice, decimal? discount, bool? isPercentate)
		{
			if (discount.GetValueOrDefault() <= 0)
				return mrpPrice;

			if (isPercentate.GetValueOrDefault() == true)
            {
				return mrpPrice - (mrpPrice * (discount.Value / 100));
			}
			else
				return mrpPrice - discount.GetValueOrDefault();   
		}
	}
}
