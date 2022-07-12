using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    [Keyless]
    public class ReturnClientInfo
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string TaxNumber { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
    }
}
