using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookModel
{
    public class BookResponseModel
    {

        [Key]
        public int BookId { get; set; }
        
        public string BookName { get; set; }
        
        public string Author { get; set; }
        
        public string Description { get; set; }
        
        public int Quantity { get; set; }
        
        public int Price { get; set; }
        
        public int DiscountedPrice { get; set; }
        
        public decimal Rating { get; set; }
        
        public int RatingCount { get; set; }
        
        public string BookImage { get; set; }
    }
}
