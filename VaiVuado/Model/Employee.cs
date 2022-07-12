using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVuado.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeementContract { get; set; }
        public decimal SalaryAmount { get; set; }
        public int PersonId { get; set; }
    }
}
