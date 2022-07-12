using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class ProductStock
    {
        public int ProductStockId { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
