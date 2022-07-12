using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ClientsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Clients
        [HttpGet]
        public async Task<List<ReturnClientInfoAll>> GetClients()
        {
            using (var context =  _appDbContext)
            {
                //var clientIdParameter = new SqlParameter("@ClientId", 4);

                var result = await context.ReturnClientInfoAlls.
                    FromSqlRaw("ReturnClientInfoAll").ToListAsync();
                return result;
            }
          //  return await _appDbContext.Client.ToListAsync();
        }

        // GET api/Clients/5 
        [HttpGet("{id}")]
        public async Task<List<ReturnClientInfo>> GetClient(int id)
        {
            using (var context = _appDbContext)
            {

                var clientIdParameter = new SqlParameter("@ClientId", id);
                var result = await context.ReturnClientInfos.
                    FromSqlRaw("ReturnClientInfo @ClientId", clientIdParameter).ToListAsync();
                return result;
            }
            //  return await _appDbContext.Client.ToListAsync();
        }

        // POST : api/Clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {

            _appDbContext.Client.Add(client);

            if (client == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetClients", new
            {
                ClientId  = client.ClientId,
                PersonId = client.PersonId
            });
        }
        //PUT : api/Clients
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {

            if (id != client.ClientId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(client).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExist(id))
                {
                    return NotFound("Client not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Client Edited");
        }
        // DELETE api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _appDbContext.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(client);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Client removed with success" });
        }
        private bool ClientExist(int id)
        {
            return _appDbContext.Client.Any(e => e.ClientId == id);
        }
    }
}
