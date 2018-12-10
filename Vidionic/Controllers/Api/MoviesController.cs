using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidionic.Dtos;
using Vidionic.Models;
using System.Data.Entity;
using Vidionic.Models;

namespace Vidionic.Controllers.Api
{
    public class MoviesController : ApiController
    {
        //private ApplicationDbContext _context;
	    private DAL dal;
        public MoviesController()
        {
            //_context = new ApplicationDbContext();
			dal=new DAL();
        }
	    public IEnumerable<MovieDto> GetMovies()
        {
			return dal.GetMovies()
				.Select(Mapper.Map<Movie, MovieDto>);
		}

        public IHttpActionResult GetMovie(int id)
        {
	        var movie = dal.GetMovieSingleOrDefaultLazyLoad(id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            //_context.Movies.Add(movie);
            //_context.SaveChanges();
			dal.AddMovie(movie);
			dal.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

	        var movieInDb = dal.GetMovieSingleOrDefaultLazyLoad(id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            //_context.SaveChanges();
			dal.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
		public IHttpActionResult DeleteMovie(int id)
        {
	        var movieInDb = dal.GetMovieSingleOrDefaultLazyLoad(id);

            if (movieInDb == null)
                return NotFound();

            //_context.Movies.Remove(movieInDb);
            //_context.SaveChanges();
			dal.DeleteMovie(movieInDb);
			dal.SaveChanges();

            return Ok();
        }
    }
}
