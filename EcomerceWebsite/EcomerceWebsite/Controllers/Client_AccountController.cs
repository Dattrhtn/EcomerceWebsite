using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcomerceWebsite.Models;

namespace EcomerceWebsite.Controllers
{
    public class Client_AccountController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Client_Account
        public ActionResult Index()
        {

            var account_id = int.Parse(Session["account_id"] as string);
            Session["name"] = db.accounts.Where(ac => ac.account_id == account_id).Select(ac => ac.first_name).FirstOrDefault();
            var currentUserId = account_id;
            var account = db.accounts
                            .FirstOrDefault(a => a.account_id == currentUserId);

            if (account == null)
            {
                return HttpNotFound("Account not found");
            }

            return View(account);

        }

        // GET: Client_Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Client_Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "account_id,first_name,last_name,email,password,address,role,phone_number,ngayTao")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
