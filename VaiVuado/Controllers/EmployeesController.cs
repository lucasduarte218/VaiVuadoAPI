using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaiVuado.Context;
using VaiVuado.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VaiVuado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _appDbContext.Employee.ToListAsync();
        }

        // GET api/Employees/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _appDbContext.Employee.FindAsync(id);
            if (!EmployeeExist(id))
            {
                return NotFound();
            }
            return employee;
        }

        // POST : api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {

            _appDbContext.Employee.Add(employee);

            if (employee == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetEmployees", new
            {
                EmployeeId = employee.EmployeeId,
                EmployeementContract = employee.EmployeementContract,
                SalaryAmount = employee.SalaryAmount,
                PersonId = employee.PersonId

            });
        }
        //PUT : api/Employees
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmploye(int id, Employee employee)
        {

            if (id != employee.EmployeeId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExist(id))
                {
                    return NotFound("Employee not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Employee Edited");
        }
        // DELETE api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _appDbContext.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(employee);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Employee removed with success" });
        }
        private bool EmployeeExist(int id)
        {
            return _appDbContext.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
