using System;
using System.Collections.Generic;
using Core.Data;

namespace MSF.Domain
{
    public class Category : BaseEntity<int>
    {
        public Category()
        {
            SubCategory = new List<Category>();
        }
        public string CategoryName { get; set; }

        public string ImageURL { get; set; }

        public List<Category> SubCategory { get; set; }
    }
}
