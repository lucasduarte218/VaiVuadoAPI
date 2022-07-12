using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaiVuado.Context;
using VaiVuado.Model;

namespace VaiVuado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PersonController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET : api/person
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPerson()
        {
            return await _appDbContext.Person.ToListAsync();
        }
        // GET : api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _appDbContext.Person.FindAsync(id);
            if (!PersonExists(id))
            {
                return NotFound();
            }
            return person;
        }
        // POST : api/Persons/5
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {

            _appDbContext.Person.Add(person);

            if (person == null)
            {
                return NotFound();
            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                TaxNumber = person.TaxNumber,
                PhoneNumber1 = person.PhoneNumber1,
                PhoneNumber2 = person.PhoneNumber2,
                AddressId = person.AddressId
            });
        }

        //PUT : api/person
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson( int id, Person person)
        {
            
            if(id != person.PersonId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(person).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound("Person not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Person Edited");
        }
        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _appDbContext.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(person);
            await _appDbContext.SaveChangesAsync();

            return Ok(new {sucess = true,data = person.PersonName + " removed with success" });
        }

        private bool PersonExists(int id)
        {
            return _appDbContext.Person.Any(e => e.PersonId == id);
        }
    }
}