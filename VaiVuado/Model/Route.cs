using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public DateTime RouteDate { get; set; }
        public int RouteStatus { get; set; }
        public int EmployeeId { get; set; }
    }
}
