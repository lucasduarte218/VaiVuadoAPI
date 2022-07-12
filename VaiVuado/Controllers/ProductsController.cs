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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _appDbContext.Product.ToListAsync();
        }

        // GET api/Products/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _appDbContext.Product.FindAsync(id);
            if (!ProductExist(id))
            {
                return NotFound();
            }
            return product;
        }

        // POST : api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {

            _appDbContext.Product.Add(product);

            if (product == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDesc = product.ProductDesc,
                ProductCat = product.ProductCat,
                BussinessOwner = product.BusinessOwner

            });
        }
        //PUT : api/Products
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {

            if (id != product.ProductId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExist(id))
                {
                    return NotFound("Product not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Product Edited");
        }
        // DELETE api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _appDbContext.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Product removed with success" });
        }
        private bool ProductExist(int id)
        {
            return _appDbContext.Product.Any(e => e.ProductId == id);
        }
    }
}
