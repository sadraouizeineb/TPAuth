using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using tpAuth.Models;
using tpAuth.Data;



namespace tpAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Afficher la liste des utilisateurs
        public IActionResult UserManagement()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }



        // Afficher la liste des films favoris (Wishlist) pour l'utilisateur connecté
        public async Task<IActionResult> WhishListPerUser()
        {
            var currentUser = _userManager.GetUserId(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var whishList = await _context.whishLists
                .Where(w => w.UserID == currentUser)
                .Include(w => w.Movie) // Inclure les détails du film
                .ToListAsync();

            return View(whishList);
        }

        // Ajouter un film aux favoris
        [HttpPost]
        public async Task<IActionResult> AjouterFavori(Guid movieId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            var exists = await _context.whishLists
                .AnyAsync(w => w.UserID == userId && w.MovieId == movieId);

            if (!exists)
            {
                var whishListItem = new WhishList
                {
                    Id = Guid.NewGuid(),
                    UserID = userId,
                    MovieId = movieId
                };

                _context.whishLists.Add(whishListItem);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Film ajouté aux favoris !";
            }
            else
            {
                TempData["ErrorMessage"] = "Ce film est déjà dans vos favoris.";
            }

            //return RedirectToAction("Index", "Movie");
            return RedirectToAction("WhishListPerUser", "Account");

        }

    }
}
