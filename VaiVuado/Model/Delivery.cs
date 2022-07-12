using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int DeliveryStatus { get; set; }
        public int ClientId { get; set; }
        public int RouteId { get; set; }
        public int ProductStockId { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
