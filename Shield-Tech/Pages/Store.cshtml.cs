using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shield_Tech.Models;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Shield_Tech.Pages
{
    public class StoreModel : PageModel
    {
        public readonly AppDataContext _db;


        public StoreModel(AppDataContext db)
        {
            _db = db;
        }


        public IList<Product> Product { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ProductCategory { get; set; }



        public void OnGet()
        {
            IQueryable<string> productQuery = from m in _db.Products
                                              orderby m.Category
                                              select m.Category;
            var product = from m in _db.Products
                          select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                product = product.Where(s => s.Productdescription.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(ProductCategory))
            {
                product = product.Where(x => x.Category == ProductCategory);

            }



            Categories = new SelectList(productQuery.Distinct().ToList());

            Product = product.ToList();
        }
    }
}


