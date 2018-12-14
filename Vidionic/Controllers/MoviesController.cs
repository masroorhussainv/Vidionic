using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidionic.Models;
using Vidionic.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidionic.Controllers
{
    public class MoviesController : Controller
    {
        //private ApplicationDbContext _context;
	    private DAL dal;

        public MoviesController()
        {
            //_context=new ApplicationDbContext();
			dal=new DAL();
        }

		// /localhost:port/movies/
        public ActionResult Index()
        {
	        if (User.IsInRole(RoleName.CanManageMovies))
	        {
		        return View("List");
	        }
			return View("ReadOnlyList");
		}

        //[Route("/movies/details/{id}")]
        public ActionResult Details(int id)
        {
	        var movie = dal.GetMovieSingleOrDefaultEagerLoad(id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

		[Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
		{
			var genres = dal.GetGenres();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

	    [Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int id)
	    {
		    var movie = dal.GetMovieSingleOrDefaultLazyLoad(id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = dal.GetGenres()
            };
            return View("MovieForm", viewModel);
        }


        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Save(Movie movie)
        {
	        var errors = ModelState
		        .Where(x => x.Value.Errors.Count > 0)
		        .Select(x => new { x.Key, x.Value.Errors })
		        .ToArray();

			//if (!ModelState.IsValid)
	  //      {
		 //       var viewModel = new MovieFormViewModel()
		 //       {
			//        Genres = dal.GetGenres()
		 //       };
		 //       return View("MovieForm", viewModel);
	  //      }


			if (movie.Id == 0)
            {
                //its a new movie to be added into db
                movie.DateAdded = DateTime.Now;
                dal.AddMovie(movie);
            }
            else
            {
                //it's an old movie being updated
	            var movieInDb = dal.GetMovieSingleLazyLoad(movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            try
            {
                dal.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex);
            }
            

            return RedirectToAction("Index", "Movies");
        }
    }
}
