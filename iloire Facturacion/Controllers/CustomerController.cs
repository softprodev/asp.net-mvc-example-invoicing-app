﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iloire_Facturacion.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private DBContext db = new DBContext();

        /*CUSTOM*/
        public ViewResultBase Search(string q)
        {
            var customers = new List<Customer>();
            if (q.Length == 1)
            {
                ViewBag.LetraAlfabetica = q;
                //alfabet, first letter
                customers = (from c in db.Customers
                                 where c.Name.StartsWith(q)
                                 select c).ToList();
            }
            else { 
                //normal search
                customers = (from c in db.Customers
                                 where c.Name.IndexOf(q)>-1
                                 select c).ToList();
            }
            
            if (Request.IsAjaxRequest())
                return PartialView("Index", customers);
            else
                return View("Index",  customers);
        }

        /*END CUSTOM*/
        
        
        //
        // GET: /Customer/

        public ViewResult Index()
        {
            return View(db.Customers.ToList());
        }

        //
        // GET: /Customer/Details/5

        public ViewResult Details(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(customer);
        }
        
        //
        // GET: /Customer/Edit/5
 
        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}