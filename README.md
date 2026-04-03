# Gestion Films

Une application web développée en ASP.NET Core MVC permettant de consulter et gérer une liste de films.

## Fonctionnalités

- **Affichage des films** : Vue d'ensemble de tous les films avec leurs informations principales (Titre, Genre, Année).
- **Filtrage par genre** : Possibilité de filtrer la liste affichée selon le genre du film (Crime, Action, Science-Fiction, etc.).
- **Tri alphabétique** : Option pour trier les films par titre de A à Z.
- **Détails d'un film** : Page dédiée affichant les informations détaillées d'un film sélectionné.
- **Page À Propos dynamique** : Affichage de statistiques calculées en temps réel sur la liste des films (nombre total, nombre de genres différents, année du film le plus ancien).
- **Gestion des messages** : Démonstration de l'utilisation des messages flash via `TempData` survivant aux redirections.

## Technologies utilisées

- **C# 14.0**
- **.NET 10**
- **ASP.NET Core MVC** (Modèle, Vue, Contrôleur)
- **HTML/CSS & Bootstrap 5** (via le Layout partagé)
- **Razor Pages / Vues Razor** 

## Structure du projet

- `Controllers/FilmController.cs` : Le contrôleur principal gérant la navigation et la logique métier de l'application (Index, Details, APropos, etc.).
- `Models/Film.cs` : La structure de données (le modèle) définissant les attributs d'un film (Id, Titre, Genre, Année).
- `Views/Film/` : Les pages de l'interface utilisateur, rendues dynamiquement grâce à Razor (`Index.cshtml`, `Details.cshtml`, `APropos.cshtml`).
- `Views/Shared/_Layout.cshtml` : Le gabarit principal contenant la barre de navigation et le pied de page du site.

## Images
<img width="835" height="875" alt="image" src="https://github.com/user-attachments/assets/d2703341-0a00-46df-b9c0-a36920ab4c01" />
<img width="937" height="878" alt="image" src="https://github.com/user-attachments/assets/36e3a1fa-7dd2-444f-ba29-d9bd3476c452" />

