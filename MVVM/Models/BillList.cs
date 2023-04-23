using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Split
{

    public class BillList
    {
        public List<Bill> Bill { get; set; }
        public BillList() {
            Bill = new List<Bill>();
        }
    }
}
