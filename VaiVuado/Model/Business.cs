using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string Cnpj { get; set; }
        public string BusinessName { get; set; }
        public int PersonId { get; set; }

    }
}
