using gestionFilms.Models;
using Microsoft.AspNetCore.Mvc;

namespace gestionFilms.Controllers
{
    public class FilmController : Controller
    {
        public static readonly List<Film> Films = new()
        {
           new Film
                {
                    Id = 1,
                    Titre = "Inception",
                    Genre = "Science Fiction",
                    Annee = 2010,
                    Description = "Un voleur infiltre les rêves pour implanter une idée."
                },

                new Film
                {
                    Id = 2,
                    Titre = "The Godfather",
                    Genre = "Crime",
                    Annee = 1972,
                    Description = "L’ascension d’un héritier dans une famille mafieuse."
                },

                new Film
                {
                    Id = 3,
                    Titre = "Pulp Fiction",
                    Genre = "Crime",
                    Annee = 1994,
                    Description = "Des histoires criminelles entremêlées à Los Angeles."
                },

                new Film
                {
                    Id = 4,
                    Titre = "Harry Potter",
                    Genre = "Fantastique",
                    Annee = 2001,
                    Description = "Un jeune sorcier découvre son destin à Poudlard."
                },

                new Film
                {
                    Id = 5,
                    Titre = "The Dark Knight",
                    Genre = "Action",
                    Annee = 2008,
                    Description = "Batman affronte le Joker qui plonge Gotham dans le chaos."
                },

                new Film
                {
                    Id = 6,
                    Titre = "The Lord of the Rings",
                    Genre = "Fantasy",
                    Annee = 2001,
                    Description = "Un hobbit doit détruire un anneau maléfique."
                }
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

          // 4. Delete : GET - Affiche la page de confirmation pour la suppression
        public IActionResult Supprimer(int id)
        {
            var film = Films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // 5. Delete : POST - Supprime réellement le film
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var film = Films.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                Films.Remove(film);
                TempData["MessageFlash"] = $"Le film '{film.Titre}' a été supprimé avec succès.";
            }

            return RedirectToAction(nameof(Index));
        }

        // 6. Add : GET - Affiche le formulaire d'ajout
        public IActionResult Ajouter()
        {
            var genres = new List<string> { "Action", "Crime", "Fantastique", "Fantasy", "Science Fiction", "Drame", "Comédie", "Horreur", "Thriller", "Animation" };
            ViewBag.Genres = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(genres);
            return View();
        }

        // 7. Add : POST - Ajoute réellement le film
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ajouter(Film film)
        {
            var genres = new List<string> { "Action", "Crime", "Fantastique", "Fantasy", "Science Fiction", "Drame", "Comédie", "Horreur", "Thriller" };

            if (ModelState.IsValid)
            {
                // Attribution du nouvel ID en fonction de ceux existants
                film.Id = Films.Any() ? Films.Max(f => f.Id) + 1 : 1;

                if (string.IsNullOrWhiteSpace(film.Description))
                {
                    film.Description = "Pas de résumé noté";
                }

                Films.Add(film);

                TempData["MessageFlash"] = $"Le film '{film.Titre}' a été ajouté avec succès !";
                return RedirectToAction(nameof(Index));
            }

            // Retourne le formulaire avec les erreurs de validation si nécessaire
            ViewBag.Genres = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(genres);
            return View(film);
        }
    }
}

