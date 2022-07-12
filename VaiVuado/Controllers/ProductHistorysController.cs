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
    public class ProductHistoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProductHistoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/ProductHistorys
        [HttpGet]
        public async Task<IEnumerable<ProductHistory>> GetProductHistorys()
        {
            return await _appDbContext.ProductHistory.ToListAsync();
        }

        // GET api/ProductHistorys/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductHistory>> GetProductHistory(int id)
        {
            var productHistory = await _appDbContext.ProductHistory.FindAsync(id);
            if (!ProductHistoryExist(id))
            {
                return NotFound();
            }
            return productHistory;
        }

        // POST : api/ProductHistorys
        [HttpPost]
        public async Task<ActionResult<ProductHistory>> PostProductHistory(ProductHistory productHistory)
        {

            _appDbContext.ProductHistory.Add(productHistory);

            if (productHistory == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetProductHistorys", new
            {
                ProductHistoryId = productHistory.ProductHistoryId,
                ProductStockId = productHistory.ProductStockId,
                Amount = productHistory.Amount,
                Date = productHistory.Date
            }); ;
        }
        //PUT : api/ProductHistorys
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductHistory(int id, ProductHistory productHistory)
        {

            if (id != productHistory.ProductHistoryId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(productHistory).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductHistoryExist(id))
                {
                    return NotFound("ProductHistory not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Product Edited");
        }
        // DELETE api/ProductHistorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductHistory(int id)
        {
            var productHistory = await _appDbContext.ProductHistory.FindAsync(id);

            if (productHistory == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(productHistory);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "ProductHistory removed with success" });
        }
        private bool ProductHistoryExist(int id)
        {
            return _appDbContext.ProductHistory.Any(e => e.ProductHistoryId == id);
        }
    }
}
