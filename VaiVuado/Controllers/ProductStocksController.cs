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
    public class ProductStockController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProductStockController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<ProductStock>> GetProductStocks()
        {
            return await _appDbContext.ProductStock.ToListAsync();
        }

        // GET api/Products/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStock>> GetProductStock(int id)
        {
            var productStock = await _appDbContext.ProductStock.FindAsync(id);
            if (!ProductStockExist(id))
            {
                return NotFound();
            }
            return productStock;
        }

        // POST : api/Products
        [HttpPost]
        public async Task<ActionResult<ProductStock>> PostProductStock(ProductStock productStock)
        {

            _appDbContext.ProductStock.Add(productStock);

            if (productStock == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetProductStocks", new
            {
                ProductStockId = productStock.ProductStockId,
                Amount = productStock.Amount,
                Status = productStock.Status,
                ProductId = productStock.ProductId,
                UpdateDate = productStock.UpdateDate

            });
        }
        //PUT : api/ProductStocks
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductStock(int id, ProductStock productStock)
        {

            if (id != productStock.ProductId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(productStock).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStockExist(id))
                {
                    return NotFound("ProductStock not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("ProductStock Edited");
        }
        // DELETE api/ProductStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductStock(int id)
        {
            var productStock = await _appDbContext.ProductStock.FindAsync(id);

            if (productStock == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(productStock);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "ProductStock removed with success" });
        }
        private bool ProductStockExist(int id)
        {
            return _appDbContext.ProductStock.Any(e => e.ProductStockId == id);
        }
    }
}
