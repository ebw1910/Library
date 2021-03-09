using LibraryCRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCRM.Models.ViewModels
{
    public class TransferViewModel
    {
        public int TransferID { get; set; }
        public ICollection<TakeLine> Lines { get; set; }

        public bool Shipped { get; set; }

        public string CardNumber { get; set; }
    }
}
