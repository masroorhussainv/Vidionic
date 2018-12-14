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
        //private ApplicationDbContext _context;

	    private DAL dal;


        public CustomersController()
        {
            //_context=new ApplicationDbContext();
			dal=new DAL();
        }
        
        // GET: /Customers
        public ActionResult Index()
        {
            return View();
        }

		// /Customers/New
        public ActionResult New()
        {
	        var membershipTypes = dal.GetMembershipTypes();
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
			//	var viewModel = new CustomerFormViewModel
			//	{
			//		Customer = customer,
			//		MembershipTypes = dal.GetMembershipTypes()
			//	};
			//	return View("CustomerForm", viewModel);
			//}

			System.Diagnostics.Debug.WriteLine("value is : "+customer.IsSubscribedToNewsletter);

			if (customer.Id == 0)
            {
                //new customer
                //add to db
                //_context.Customers.Add(customer);
				dal.AddCustomer(customer);
            }
            else
            {
                var customerInDb = dal.GetCustomerSingleLazyLoad(customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.Name = customer.Name;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.MembershipType = customer.MembershipType;
            }
            //_context.SaveChanges();
			dal.SaveChanges();

            return RedirectToAction("Index","Customers");
        }

        public ActionResult Details(int id)
        {
	        var customer = dal.GetCustomerSingleOrDefaultEagerLoad(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
	        var customer = dal.GetCustomerSingleOrDefaultLazyLoad(id);
            if (customer == null)
                HttpNotFound();
            var viewModel =new CustomerFormViewModel
            {
                Customer=customer,
                MembershipTypes= dal.GetMembershipTypes()
            };
            return View("CustomerForm", viewModel);
        }
    }
}