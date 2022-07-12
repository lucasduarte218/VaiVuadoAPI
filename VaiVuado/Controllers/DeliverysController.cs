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
    public class DeliveryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public DeliveryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Deliverys
        [HttpGet]
        public async Task<IEnumerable<Delivery>> GetDeliverys()
        {
            return await _appDbContext.Delivery.ToListAsync();
        }

        // GET api/Deliverys/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var delivery = await _appDbContext.Delivery.FindAsync(id);
            if (!DeliveryExist(id))
            {
                return NotFound();
            }
            return delivery;
        }

        // POST : api/Deliverys
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {

            _appDbContext.Delivery.Add(delivery);

            if (delivery == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetDeliverys", new
            {
                DeliveryId = delivery.DeliveryId,
                DeliveryStatus = delivery.DeliveryStatus,
                ClientId = delivery.ClientId,
                RouteId = delivery.RouteId,
                ProductStockId = delivery.ProductStockId,
                DeliveryDate = delivery.DeliveryDate

            });
        }
        //PUT : api/Deliverys
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery)
        {

            if (id != delivery.DeliveryId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExist(id))
                {
                    return NotFound("Delivery not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Delivery Edited");
        }
        // DELETE api/Deliverys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _appDbContext.Delivery.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(delivery);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Delivery removed with success" });
        }
        private bool DeliveryExist(int id)
        {
            return _appDbContext.Delivery.Any(e => e.DeliveryId == id);
        }
    }
}
