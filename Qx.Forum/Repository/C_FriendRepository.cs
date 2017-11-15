using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class C_FriendRepository : BaseRepository,IRepository<BBS_C_Friend>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            throw new NotImplementedException();
        }

        public string Add(BBS_C_Friend model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id)
        {
           
            throw new NotImplementedException();
        }

        public bool Update(BBS_C_Friend model, string note = "")
        {
            
            throw new NotImplementedException();
        }

        public BBS_C_Friend Find(object id)
        {
            throw new NotImplementedException();
        }

        public List<BBS_C_Friend> All()
        {
            throw new NotImplementedException();
        }

        public List<BBS_C_Friend> All(Func<BBS_C_Friend, bool> filter)
        {
            throw new NotImplementedException();
        }

        public BBS_C_Friend Find(object[] id)
        {
            throw new NotImplementedException();
        }
    }
}
