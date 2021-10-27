using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using marketplace.Models;

namespace marketplace.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MarketContext _context;

        public ProductController(MarketContext context)
        {
            _context = context;
        }

        // GET: v1/product/4
        [HttpGet("{id}")]
        public async Task<ActionResult<product>> Getproduct(long id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: v1/product/4
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproduct(long id, [FromForm] product product)
        {
            var originalProduct = await _context.products.FindAsync(id);

            if (originalProduct == null)
            {
                return NotFound();
            }

            product.Id = id;
            if (product.Name == null)
            {
                product.Name = originalProduct.Name;
            }
            if (product.Price == null)
            {
                product.Price = originalProduct.Price;
            }

            var local = _context.Set<product>()
                .Local
                .FirstOrDefault(p => p.Id.Equals(id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(200);
        }

        // POST: v1/product
        [HttpPost]
        public async Task<ActionResult<product>> Postproduct([FromForm] product product)
        {
            _context.products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        // DELETE: v1/product/4
        [HttpDelete("{id}")]
        public async Task<ActionResult<product>> Deleteproduct(long id)
        {
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool productExists(long id)
        {
            return _context.products.Any(e => e.Id == id);
        }
    }
}
