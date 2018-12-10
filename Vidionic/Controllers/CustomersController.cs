using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidionic.Models;
using System.Data.Entity;
using Vidionic.ViewModels;

namespace Vidionic.Controllers
{
	[Authorize(Roles = RoleName.CanManageMovies)]
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

       

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            CustomerFormViewModel formViewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",formViewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
			//if (!ModelState.IsValid)
			//{
			//    var viewModel = new CustomerFormViewModel
			//    {
			//        Customer=customer,
			//        MembershipTypes = _context.MembershipTypes.ToList()
			//    };
			//    return View("CustomerForm", viewModel);
			//}

			System.Diagnostics.Debug.WriteLine("value is : "+customer.IsSubscribedToNewsletter);

			if (customer.Id == 0)
            {
                //new customer
                //add to db
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.MembershipType = customer.MembershipType;

            }
            _context.SaveChanges();

            return RedirectToAction("Index","Customers");
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

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                HttpNotFound();
            var viewModel =new CustomerFormViewModel
            {
                Customer=customer,
                MembershipTypes=_context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}