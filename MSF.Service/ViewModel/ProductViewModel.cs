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

		public decimal Discount { get; set; }

		public bool IsDiscountInPercentage { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public int MinimumQuantity { get; set; }

		public int TaxId { get; set; }

		public string TaxName { get; set; }

		public int UOMId { get; set; }

		public string UOMName { get; set; }

		public int BulkUOMId { get; set; }

		public int BulkUOMQty { get; set; }

		public int OpeningStock { get; set; }

		public string RowVersion { get; set; }

		public static implicit operator ProductViewModel(Product product)
		{
			return new ProductViewModel {
				 ProductId = product.ID,
				 ProductCode = product.ProductCode,
				 ProductImage = Convert.ToBase64String(product.ProductImage),
				 ProductName = product.ProductName,
				 MRPPrice = product.MRPPrice,
				 DisplayName= product.DisplayName,
				 Discount=product.Discount,
				 RowVersion = Convert.ToBase64String( product.RowVersion ),
				 CategoryId = product.CategoryId,
				 CategoryName = product.Category.CategoryName,
				 MinimumQuantity = product.MinimumQuantity,
				 UOMId = product.UOMId ,
				 UOMName = product.UOM.UOMAbbr,
				 TaxId = product.TaxId,
				 TaxName = product.Tax.TaxName,
				 OpeningStock = product.OpeningStock
			};
		}

		public static implicit operator Product(ProductViewModel viewModel)
		{
			return new Product 
			{
				ID = viewModel.ProductId,
				ProductCode = viewModel.ProductCode,
				ProductImage = Convert.FromBase64String(viewModel.ProductImage),
				ProductName = viewModel.ProductName,
				MRPPrice = viewModel.MRPPrice,
				DisplayName = viewModel.DisplayName,
				Discount = viewModel.Discount,
				RowVersion = Convert.FromBase64String(viewModel.RowVersion),
				CategoryId = viewModel.CategoryId,
				MinimumQuantity = viewModel.MinimumQuantity,
				UOMId = viewModel.UOMId,
				TaxId = viewModel.TaxId,
				BulkUOMId = viewModel.BulkUOMId,
				BulkUOMQty=viewModel.BulkUOMQty,
				IsDiscountInPercentage = viewModel.IsDiscountInPercentage,
				OpeningStock = viewModel.OpeningStock
			};
		}
	}
}
