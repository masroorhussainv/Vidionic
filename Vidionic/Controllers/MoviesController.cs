using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidionic.Models;
using Vidionic.ViewModels;

namespace Vidionic.Controllers
{
    public class MoviesController : Controller
    {
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

        public ActionResult Edit(int id)
        {
            return View();
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

        private IEnumerable<Movie> getMovies()
        {
            return new List<Movie>
            {
                new Movie {Name = "Snatch"},
                new Movie {Name = "Avengers: Infinity War"},
                new Movie {Name = "Lord of the Rings"}
            };
        }

        public ActionResult Index()
        {
            IEnumerable<Movie> movies = getMovies();

            return View(movies);
        }


        [Route("movies/released/{year}/{month:range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content("Year "+year+", "+"Month "+month);
        }
        

    }
}