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
    public class AddressesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public AddressesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/<AddressesController>
        [HttpGet]
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _appDbContext.Address.ToListAsync();
        }

        // GET api/<AddressesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAdress(int id)
        {
            var address = await _appDbContext.Address.FindAsync(id);
            if (!AddressExist(id))
            {
                return NotFound();
            }
            return address;
        }

        // POST : api/Addresses/5
        [HttpPost]
        public async Task<ActionResult<Address>> PostAdress(Address address)
        {

            _appDbContext.Address.Add(address);

            if (address == null)
            {
                return NotFound();

            }
            if (address.FederalState.Length > 2)
            {
                return ValidationProblem("Invalid Input: FederalState need have only 2 chars");
            }
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetAddresses", new
            {
                AddressId = address.AddressId,
                ZIPcode = address.ZIPcode,
                FederalState = address.FederalState,
                City = address.City, 
                Discrict = address.District,
                Street = address.Street,
                Number = address.Number

            }) ;
        }
        //PUT : api/address
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Person address)
        {

            if (id != address.AddressId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(address).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExist(id))
                {
                    return NotFound("Address not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Address Edited");
        }
        // DELETE api/<AddressesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdress(int id)
        {
            var address = await _appDbContext.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(address);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Address removed with success" });
        }
        private bool AddressExist(int id)
        {
            return _appDbContext.Address.Any(e => e.AddressId == id);
        }
    }
}
