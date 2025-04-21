using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

using tpAuth.Data;

namespace tpAuth.Controllers
{
  
public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MovieController( ApplicationDbContext context)
        {
            
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }
    }

}

