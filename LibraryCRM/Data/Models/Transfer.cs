using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryCRM.Data.Models
{
    public class Transfer
    {

        [BindNever]
        public int TransferID { get; set; }
        [BindNever]
        public ICollection<TakeLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Please enter a card number")]
        public string CardNumber { get; set; }
    }
}
