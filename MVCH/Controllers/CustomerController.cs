using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCH.Models;

namespace MVCH.Controllers
{
    public class CustomerController : Controller
    {
        private readonly 客戶資料Repository _CustomerRepo;
        private readonly VW_客戶聯絡人跟銀行資訊統計Repository _CountInfoRepo;
        private readonly 客戶分類Repository _CustomerTypeRepo;

        public CustomerController()
        {
            this._CustomerRepo = RepositoryHelper.Get客戶資料Repository();
            this._CountInfoRepo = RepositoryHelper.GetVW_客戶聯絡人跟銀行資訊統計Repository();
            this._CustomerTypeRepo = RepositoryHelper.Get客戶分類Repository();
        }

        // GET: Customer
        public ActionResult Index(string search,int? customerType)
        {
            var data = this._CustomerRepo.Search(search,customerType);

            ViewBag.CustomerType = new SelectList(this._CustomerTypeRepo.All(), "Id", "分類名稱");
            return View(data);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = this._CustomerRepo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = new SelectList(this._CustomerTypeRepo.All(), "Id", "分類名稱");
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                this._CustomerRepo.Add(客戶資料);
                this._CustomerRepo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶分類 = new SelectList(this._CustomerTypeRepo.All(), "Id", "分類名稱", 客戶資料.客戶分類);
            return View(客戶資料);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = this._CustomerRepo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶分類 = new SelectList(this._CustomerTypeRepo.All(), "Id", "分類名稱",客戶資料.客戶分類);
            return View(客戶資料);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ((客戶資料Entities)this._CustomerRepo.UnitOfWork.Context).Entry(客戶資料).State = EntityState.Modified;
                this._CustomerRepo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶分類 = new SelectList(this._CustomerTypeRepo.All(), "Id", "分類名稱", 客戶資料.客戶分類);
            return View(客戶資料);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = this._CustomerRepo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = this._CustomerRepo.Find(id);
            客戶資料.已刪除 = true;

            foreach (var contact in 客戶資料.客戶聯絡人)
            {
                contact.已刪除 = true;
            }

            foreach (var bankInfo in 客戶資料.客戶銀行資訊)
            {
                bankInfo.已刪除 = true;
            }

            this._CustomerRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult CountInfo()
        {
            return View(this._CountInfoRepo.All());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._CustomerRepo.UnitOfWork.Context.Dispose();
                this._CountInfoRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
