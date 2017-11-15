using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.CPQ.Entity;
using Qx.CPQ.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.CPQ.Controllers
{
    public abstract class BaseCPQController : BaseAccountController
    {
        private CPQDbContext _db;
        public CpqService _cpqService{get {return  new CpqService();}}
        public CRUDService _crudService { get { return new CRUDService(); } }
        public CPQDbContext Db
        {
            get
            {
                if (_db == null)
                {
                   
                    _db = new CPQDbContext();
                }
                return _db;
            }
        }

    }
}
