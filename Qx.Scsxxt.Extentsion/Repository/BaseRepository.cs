using System;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class BaseRepository
    {
        private MyContext db;
      
        protected MyContext Db
        {
            get
            {
                if (db == null)
                {
                    db = new MyContext();
                }
                return db;
            }
        }


        protected string Pk
        {
            get { return DateTime.Now.Random().ToString(); }
        }
    }
}