using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PP2_JewelryApp_05092016;

namespace PP2_JewelryApp_05092016.Controllers
{
    public class ItemsController : Controller
    {
        private JewelryDBEntities db = new JewelryDBEntities();

        //GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Collection);
            return View(items.ToList());
        }

        // Attempted replace original Index() action with Index() with parameter to filter view
        //public ActionResult Index(string type)
        //{
        //    if (type == null)
        //    {
        //        var items = db.Items.Include(i => i.Collection);
        //        return View(items.ToList());
        //    }
        //    else
        //    {
        //        var items = db.Items.Where(i => i.Type == type);
        //        return View(items.ToList());
        //    }
        //}


        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,CollectionID,Name,Photo,Cost,StyleNum,Type,Description,Notes,Retired,Own")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "Name", item.CollectionID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "Name", item.CollectionID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,CollectionID,Name,Photo,Cost,StyleNum,Type,Description,Notes,Retired,Own")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollectionID = new SelectList(db.Collections, "CollectionID", "Name", item.CollectionID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
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
