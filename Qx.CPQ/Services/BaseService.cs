using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qx.CPQ.Entity;
using Qx.Tools.CommonExtendMethods;
namespace Qx.Wbs.Services
{
  public  class BaseService
  {
      private CPQDbContext _db;

      protected CPQDbContext Db
      {
          get
          {
              if (_db==null)
              {
                  _db = new CPQDbContext();
              }
              return _db;
          }
      }
      protected int PKInt { get { return DateTime.Now.ToInt(); } }
      protected string PK { get { return DateTime.Now.Random2(); } }
    }
}
