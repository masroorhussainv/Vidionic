using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vidionic.Models
{
	public class DAL
	{
		ApplicationDbContext _context;

		public DAL()
		{
			_context=new ApplicationDbContext();
		}

		public IEnumerable<MembershipType> GetMembershipTypes()
		{
			return _context.MembershipTypes.ToList();
		}

		public void AddCustomer(Customer customer)
		{
			_context.Customers.Add(customer);
		}

		public void DeleteCustomer(Customer customer)
		{
			_context.Customers.Remove(customer);
		}

		public IEnumerable<Customer> GetCustomersEagerLoad()
		{
			return _context.Customers
				.Include(c => c.MembershipType)
				.ToList();
		} 


		public Customer GetCustomerSingleLazyLoad(int id)
		{
			return _context.Customers.Single(c => c.Id == id);
		}

		public Customer GetCustomerSingleOrDefaultEagerLoad(int id)
		{
			return _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public Customer GetCustomerSingleOrDefaultLazyLoad(int id)
		{
			return _context.Customers.SingleOrDefault(c => c.Id == id);
		}


		//Movies DAL functions

		public IEnumerable<Movie> GetMovies()
		{
			return _context.Movies
				.Include(m => m.Genre)
				.ToList();
		}

		public Movie GetMovieSingleOrDefaultEagerLoad(int id)
		{
			return _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
		}

		public Movie GetMovieSingleOrDefaultLazyLoad(int id)
		{
			return _context.Movies.SingleOrDefault(c => c.Id == id);
		}

		public void AddMovie(Movie movie)
		{
			_context.Movies.Add(movie);
		}

		public void DeleteMovie(Movie movie)
		{
			_context.Movies.Remove(movie);
		}

		public Movie GetMovieSingleLazyLoad(int id)
		{
			return _context.Movies.Single(m => m.Id == id);
		}



		//Genre DAL functions
		public IEnumerable<Genre> GetGenres()
		{
			return _context.Genres.ToList();
		}


	}
}