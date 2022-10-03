using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace CommonLayer.AddressModel
{
    public class AddressDataModel
    {
        [Required]
        [DefaultValue("1")]
        [Range(1, 3, ErrorMessage = "Choose Address Types As 1 : Home , 2 : Office , 3 : Other")]
        public int AddressType { get; set; }

        
        public string FullAddress { get; set; }

        
        public string City { get; set; }

        
        public string State { get; set; }
    }
}
