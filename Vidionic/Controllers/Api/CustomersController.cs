using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using Vidionic.Dtos;
using Vidionic.Models;

namespace Vidionic.Controllers.Api
{
	[Authorize(Roles = RoleName.CanManageMovies)]
    public class CustomersController : ApiController
    {
        //private ApplicationDbContext _context;

	    private DAL dal;

        public CustomersController()
        {
            //_context = new ApplicationDbContext();
			dal=new DAL();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers(string query=null)
        {
			ApplicationDbContext _context=new ApplicationDbContext();
			var customersQuery = _context.Customers
		        .Include(c => c.MembershipType);

	        if (!String.IsNullOrWhiteSpace(query))
		        customersQuery = customersQuery.Where(c => c.Name.Contains(query));

	        var customerDtos = customersQuery
		        .ToList()
		        .Select(Mapper.Map<Customer, CustomerDto>);

	        return Ok(customerDtos);
		}

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
			var customer = dal.GetCustomerSingleOrDefaultLazyLoad(id);

			if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            //_context.Customers.Add(customer);
			dal.AddCustomer(customer);
            //_context.SaveChanges();
			dal.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = dal.GetCustomerSingleOrDefaultLazyLoad(id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

           dal.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
	        var customerInDb = dal.GetCustomerSingleOrDefaultLazyLoad(id);

            if (customerInDb == null)
                return NotFound();

            //_context.Customers.Remove(customerInDb);
			dal.DeleteCustomer(customerInDb);
            //_context.SaveChanges();
			dal.SaveChanges();
            return Ok();
        }
    }
}
