using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class ProductHistory
    {
        public int ProductHistoryId { get; set; }
        public int ProductStockId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
