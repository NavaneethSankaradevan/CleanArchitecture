using System;
using Core.Data;

namespace MSF.Domain
{
	public class Product : BaseEntity<long>
	{

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string DisplayName { get; set; }

        public byte[] ProductImage { get; set; }

        public decimal MRPPrice { get;  set; }

        public decimal? Discount { get; set; }

        public bool? IsDiscountInPercentage { get; set; }

        public int CategoryId { get;  set; }

		public virtual Category Category { get;  set; }

        public int MinimumQuantity { get; set; }

        public int TaxId { get; set; }

        public virtual Tax Tax { get; set; }

        public int UOMId { get; set; }

        public virtual UOM UOM { get; set; }

        public int? BulkUOMId { get; set; }

        public int? BulkUOMQty { get; set; }

        public int OpeningStock { get; set; }
    }
}
