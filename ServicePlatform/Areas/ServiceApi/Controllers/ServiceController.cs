using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.ServiceApi.Controllers
{
    public class ServiceController : BaseController
    {
        //
        // GET: /ServiceApi/Service/Logo?Ent_No=

        public string Logo(string Ent_No)
        {
            DataContext.SetFiled("UnitID", Ent_No);
            var logo = S<Core.Services.Entity.T_DownLoadFiles>().All(DataContext, "某单位的Logo").FirstOrDefault();
            return logo == null ? "#" : logo.DLFileUrl;
        }

        //
        // GET: /ServiceApi/Service/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ServiceApi/Service/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ServiceApi/Service/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ServiceApi/Service/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ServiceApi/Service/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ServiceApi/Service/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ServiceApi/Service/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
