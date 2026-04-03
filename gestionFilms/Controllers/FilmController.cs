using gestionFilms.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestionFilms.Controllers
{
    public class FilmController : Controller
    {
        public readonly List<Film> Films = new()
        {
            new Film { Id = 1, Titre = "Inception", Genre = "Science Fiction", Annee = 2010 },
            new Film { Id = 2, Titre = "The Godfather", Genre = "Crime", Annee = 1972 },
            new Film { Id = 3, Titre = "Pulp Fiction", Genre = "Crime", Annee = 1994 },
            new Film { Id = 4, Titre = "Harry Potter", Genre = "Fantastique", Annee = 2001 },
            new Film { Id = 5, Titre = "The Dark Knight", Genre = "Action", Annee = 2008 },
            new Film { Id = 6, Titre = "The Lord of the Rings", Genre = "Fantasy", Annee = 2001 }
        };

        //1.Index(string? genre = null) : affiche tous les films ou ceux correspondant au genre passé en paramètre
        //Bonus : trier les films par titre dans l’ordre croissant si un paramètre de tri est passé (ex : ?sortOrder=titre_asc)
        public IActionResult Index(string? genre = null, string? sortOrder = null)
        {
            List<Film> filmFiltrer = string.IsNullOrEmpty(genre) ? Films :
                Films.Where(f => f.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (sortOrder == "titre_asc")
            {
                filmFiltrer = filmFiltrer.OrderBy(f => f.Titre).ToList();
            }

            ViewData["CurrentGenre"] = genre;
            return View(filmFiltrer);
        }

        //2.Details(int id) : affiche les détails d’un film spécifique
        public IActionResult Details(string genre, int id)
        {
            Film? film = Films.FirstOrDefault(g => g.Id == id && g.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            if (film == null)
            {
                //404 Not Found
                return NotFound();
            }
            return View(film);
        }

        //3.APropos() : affiche une page avec des statistiques simples et un éventuel message persistant
        public IActionResult APropos()
        {
            ViewBag.NombreTotal = Films.Count;
            ViewBag.NombreGenres = Films.Select(f => f.Genre).Distinct().Count();
            ViewBag.AnneePlusAncienne = Films.Min(f => f.Annee);
            ViewBag.Message = "Bienvenue sur la page 'À propos' !";

            return View();
        }

        // Action de test pour démontrer le TempData
        public IActionResult RedirectionAvecMessage()
        {
            TempData["MessageFlash"] = "Succès : Ce message via TempData a survécu à la redirection !";
            return RedirectToAction(nameof(APropos));
        }
    }
}

