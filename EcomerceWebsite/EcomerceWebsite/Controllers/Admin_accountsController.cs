﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcomerceWebsite.Models;

namespace EcomerceWebsite.Controllers
{
    public class Admin_accountsController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Admin_accounts
        public ActionResult Index()
        {
            return View(db.accounts.ToList());
        }

        // GET: Admin_accounts/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin_accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "account_id,first_name,last_name,email,password,address,role,phone_number")] account account)
        {
            if (ModelState.IsValid)
            {
                account.ngayTao = DateTime.Now;
                db.accounts.Add(account);
                db.SaveChanges();
                TempData["erAddAc"] = "Thêm tài khoản thành công!";
                ViewBag.erAddAc = TempData["erAddAc"] as string;
                return View();
            }

            return View(account);
        }

        // GET: Admin_accounts/Edit/5
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

        // POST: Admin_accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "account_id,first_name,last_name,email,password,address,role,phone_number")] account account)
        {
            var existingPayment = db.accounts.AsNoTracking().FirstOrDefault(p => p.account_id == account.account_id);
            if (ModelState.IsValid)
            {
                account.ngayTao = existingPayment.ngayTao; // Giữ nguyên giá trị ngayTao
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Admin_accounts/Delete/5
        public ActionResult Delete(int? id)
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
            ViewBag.erDeleteAc = TempData["erDeleteAc"] as string;
            return View(account);
        }

        // POST: Admin_accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            try { 
                
                db.accounts.Remove(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["erDeleteAc"] = "Tài khoản đang được sử dụng!";
                return RedirectToAction("Delete", new { id = account.account_id});
            }
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
