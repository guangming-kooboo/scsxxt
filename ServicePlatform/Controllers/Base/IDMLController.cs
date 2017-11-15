using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;


namespace ServicePlatform.Controllers.Base
{
    public interface IDMLController<T>
    {
        IDML<T> Services { get; }
        // GET: {Controller}
        ActionResult Index(string id);

        // GET: {Controller}/Details/5
        ActionResult Details(string id);

        // GET: {Controller}/Create
        ActionResult Create();

        // POST: {Controller}/Create
        [HttpPost]
        ActionResult Create(T model);

        // GET: {Controller}/Edit/5
        ActionResult Edit(string id);

        // POST: {Controller}/Edit/5
        [HttpPost]
        ActionResult Edit(string id, T model);

        // GET: {Controller}/Delete/5
        ActionResult Delete(string id);

        // POST: {Controller}/Delete/5
        [HttpPost]
        ActionResult Delete(string id, T model);
    }
}