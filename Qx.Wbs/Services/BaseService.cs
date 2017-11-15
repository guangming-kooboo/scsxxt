using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Entity;

namespace Qx.Wbs.Services
{
  public  class BaseService
    {
      private WbsDbContext _db;
      protected WbsDbContext Db
        {
          get
          {
              if (_db==null)
              {
                  _db=new WbsDbContext();
              }
              return _db;
              
          }
        }
        protected string SerialId
        {
            get
            {
                return DateTime.Now.Random().ToString();

            }
        }
    }

   
}
