using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidionic.Models;
using System.Data.Entity;

namespace Vidionic.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context=new ApplicationDbContext();
        }
        //applicationdb context is a disposable object
        //therefore to dispose it, override base class (COntroller's) dispose method
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //private IEnumerable<Customer> getCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer {Name = "Masroor",Id = 1},
        //        new Customer { Name = "Hasan" ,Id=2},
        //        new Customer { Name = "Abdul Wakeel",Id=3 }
        //    };
        //}

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
    }
}