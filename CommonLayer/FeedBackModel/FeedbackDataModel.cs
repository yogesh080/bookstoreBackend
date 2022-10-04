using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace CommonLayer.FeedBackModel
{
    public class FeedbackDataModel
    {
        
        [DefaultValue("0")]
        public int BookId { get; set; }

        public decimal TotalRating { get; set; }

        public string Comment { get; set; }
    }
}
