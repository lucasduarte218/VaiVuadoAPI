using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class Address
    {

        public int AddressId { get; set; }
        public int ZIPcode { get; set; }
        public string FederalState { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

    }
}
