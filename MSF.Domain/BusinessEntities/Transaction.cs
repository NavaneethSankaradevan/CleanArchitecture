using Core.Data;
using System;
using System.Collections.Generic;

namespace MSF.Domain
{
    public class Transaction:BaseEntityTrackable<long>
    {
        public Transaction()
        {
            TransactionDetails = new List<TransactionDetails>();
        }

        public string OrderNumber { get; set; }

        public int CustomerId { get; set; }

        public DateTime TranDate { get; set; }

        public decimal TransactionAmount { get; set; }

        public decimal TaxValue { get; set; }

        public decimal DiscountValue { get; set; }

        public virtual List<TransactionDetails> TransactionDetails { get; set; }

    }

    public class TransactionDetails:BaseEntityTrackable<long>
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal OrderPrice { get; set; }

        public int TaxId { get; set; }

        public decimal TaxValue { get; set; }

        public decimal DiscountValue { get; set; }

        public virtual Product Product { get; set; }

        public virtual Transaction Order { get; set; }
    }
}
