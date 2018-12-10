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
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie=new Movie() {Name = "Shrek"};
            List<Customer> customers=new List<Customer>
            {
                new Customer { Name="Customer 1"},
                new Customer { Name = "Customer 2"},
                new Customer { Name="Customer 3"},
                new Customer { Name = "Customer 4"},
                new Customer { Name="Customer 5"},
                new Customer { Name = "Customer 6"},
            };

            var vm=new RandomMovieViewModel();
            vm.Movie = movie;
            vm.Customers = customers;

            return View(vm);
        }
        

        //public ActionResult Index(int? pageIndex,string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (sortBy.IsNullOrWhiteSpace())
        //    {
        //        sortBy = "Name";
        //    }

        //    return View();
        //}

      
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
            var movie = _context.Movies.Include(m=>m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }



        [Route("movies/released/{year}/{month:range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content("Year "+year+", "+"Month "+month);
        }


		[Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

	    [Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }


        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                //its a new movie to be added into db
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                //it's an old movie being updated
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex);
            }
            

            return RedirectToAction("Index", "Movies");
        }
    }
}
