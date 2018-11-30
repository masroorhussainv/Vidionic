using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidionic.Models;

namespace Vidionic.Controllers
{
    public class CustomersController : Controller
    {

        private IEnumerable<Customer> getCustomers()
        {
            return new List<Customer>
            {
                new Customer {Name = "Masroor",Id = 1},
                new Customer { Name = "Hasan" ,Id=2},
                new Customer { Name = "Abdul Wakeel",Id=3 }
            };
        }

        // GET: Customers
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = getCustomers();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            Customer c1 = new Customer {Name = "Masroor",Id = 1};
            Customer c2 = new Customer { Name = "Hasan" ,Id=2};
            Customer c3 = new Customer { Name = "Abdul Wakeel",Id=3 };

            if (id==1)
            {
                return View(c1);
            }
            else if(id==2)
            {
                return View(c2);
            }
            else if (id == 3)
            {
                return View(c3);
            }
            return View();
        }
    }
}