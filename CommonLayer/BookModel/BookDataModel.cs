using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.BookModel
{
    public class BookDataModel
    {
        public string BookName { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }

        public double Rating { get; set; }

        public int RatingCount { get; set; }

        public string BookImage { get; set; }
    }
}
