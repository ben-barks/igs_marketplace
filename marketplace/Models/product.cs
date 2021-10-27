using System;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Models
{
    public class product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
