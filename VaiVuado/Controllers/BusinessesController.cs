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
    public class BusinessesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BusinessesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Businesses
        [HttpGet]
        public async Task<IEnumerable<Business>> GetBusinesses()
        {
            return await _appDbContext.Business.ToListAsync();
        }

        // GET api/<Businesses>/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBussiness(int id)
        {
            var business = await _appDbContext.Business.FindAsync(id);
            if (!BusinessExist(id))
            {
                return NotFound();
            }
            return business;
        }

        // POST : api/Businesses
        [HttpPost]
        public async Task<ActionResult<Business>> PostBusiness(Business business)
        {

            _appDbContext.Business.Add(business);

            if (business == null)
            {
                return NotFound();

            }
           
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetBusinesses", new
            {
                BussinessId = business.BusinessId,
                BussinessCnpj = business.Cnpj,
                BussinessName = business.BusinessName,
                PersonId = business.PersonId
            });
        }
        //PUT : api/businesses
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusiness(int id, Business business)
        {

            if (id != business.BusinessId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(business).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExist(id))
                {
                    return NotFound("Business not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Business Edited");
        }
        // DELETE api/Businesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            var business = await _appDbContext.Business.FindAsync(id);

            if (business == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(business);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Business removed with success" });
        }
        private bool BusinessExist(int id)
        {
            return _appDbContext.Business.Any(e => e.BusinessId == id);
        }
    }
}
