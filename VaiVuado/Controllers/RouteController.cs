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
    public class RouteController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public RouteController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: api/Routes
        [HttpGet]
        public async Task<IEnumerable<Route>> GetRoutes()
        {
            return await _appDbContext.Route.ToListAsync();
        }
        // GET api/Routes/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Route>> GetRoute(int id)
        {
            var route = await _appDbContext.Route.FindAsync(id);
            if (!RouteExist(id))
            {
                return NotFound();
            }
            return route;
        }

        // POST : api/Routes
        [HttpPost]
        public async Task<ActionResult<Route>> PostRoute(Route route)
        {

            _appDbContext.Route.Add(route);

            if (route == null)
            {
                return NotFound();

            }

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetRoutes", new
            {
                RouteId = route.RouteId,
                RouteName = route.RouteName,
                RouteDate = route.RouteDate,
                RouteStatus = route.RouteDate,
                EmployeeId = route.EmployeeId

            });
        }
        //PUT : api/Routes
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(int id, Route route)
        {

            if (id != route.RouteId)
            {
                return BadRequest("Invalid Id");
            }
            _appDbContext.Entry(route).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExist(id))
                {
                    return NotFound("Route not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Route Edited");
        }
        // DELETE api/Routes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _appDbContext.Route.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(route);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { sucess = true, data = "Route removed with success" });
        }
        private bool RouteExist(int id)
        {
            return _appDbContext.Route.Any(e => e.RouteId == id);
        }
    }
}
