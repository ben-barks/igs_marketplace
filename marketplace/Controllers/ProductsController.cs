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
    public class ProductsController : ControllerBase
    {
        private readonly MarketContext _context;

        public ProductsController(MarketContext context)
        {
            _context = context;
        }

        // GET: v1/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product>>> Getproducts()
        {
            return await _context.products.ToListAsync();
        }
    }
}
