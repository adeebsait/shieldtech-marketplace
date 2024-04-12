using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shield_Tech.Models;

namespace Shield_Tech.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly Shield_Tech.Models.AppDataContext _context;

        public IndexModel(Shield_Tech.Models.AppDataContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
