using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split
{
    public class Bill
    {
        public decimal Cost { get; set; }
        public bool IsBoth { get; set; }

        public Bill(decimal Cost , bool IsBoth) {
            this.Cost = Cost;
            this.IsBoth = IsBoth;
        }
    }
}
