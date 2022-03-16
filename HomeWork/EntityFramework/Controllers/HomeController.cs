using EntityFramework.Data;
using EntityFramework.Models;
using EntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail details = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            Settings settings = await _context.Settings.FirstOrDefaultAsync();
            List<Product> products = await _context.Products
                .Include(m => m.Category)
                .Include(m => m.Images)
                .OrderByDescending(m => m.Id)
                .Skip(1)
                .Take(settings.HomeProductTake)
                .ToListAsync();
            About abouts = await _context.Abouts
                .Include(i=>i.Advantages)
                .FirstOrDefaultAsync();
            ExpertSection expertSection = await _context.ExpertSections.FirstOrDefaultAsync();
            List<Expert> experts = await _context.Experts
                .Include(i => i.Image)
                .ToListAsync();
            List<Expert> expertsThoughts = await _context.Experts
                .Include(i => i.Image)
                .OrderBy(m=>m.Id)
                .Take(2)
                .OrderByDescending(m=>m.Id)
                .ToListAsync();
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            List<Instagram> instagrams = await _context.Instagrams.ToListAsync();
            



            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Detail = details,
                Categories = categories,
                Products = products,
                Abouts = abouts,
                Experts = experts,
                ExpertsThoughts =expertsThoughts,
                ExpertSection = expertSection,
                Blogs = blogs,
                Instagrams = instagrams
            };
            return View(homeVM);
        }
    }
}
