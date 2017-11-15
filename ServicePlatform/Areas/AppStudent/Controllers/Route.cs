using System.Collections.Generic;
using System.Linq;

namespace ServicePlatform.Areas.AppStudent.Controllers
{
    public class FriendRoute: Route
    {
        public string _condition;
        public FriendRoute(string action, string controller, string area, object param = null, string condition="") : 
            base(action, controller, area, param)
        {
            _condition = condition;
        }
        

    }
    public class Route
    {
        public string _action;
        public string _controller;
        public string _area;
        public object _param;
        public List<FriendRoute> _friends;
        public Route(string action, string controller, string area, object param = null)
        {
            this._action = action;
            this._controller = controller;
            this._area = area;
            this._param = param ?? new object();
            _friends = new List<FriendRoute>();
        }
       

        public string ToUrl(object routeParam)
        {
            if (routeParam == null)
                return ID;

            var temp = Qx.Tools.ReflectionUtility.GetObjInfo(routeParam);
            var dest = "";
            for (var i = 0; i < temp[0].Count(); i ++)
            {
                dest += temp[0][i]+"=" + temp[1][i]+"&";
            }
            return ID + "?"+ dest;
        }
        public string ID
        {
            get
            {
                var id = "/" + _area + "/" + _controller + "/" + _action;
                return id;
            }
        }
        public void AddFriend(FriendRoute friend)
        {
            _friends.Add(friend);
        }
        public void AddFriend(Route friend,string condition)
        {
            var friendRoute = new FriendRoute(friend._action,friend._controller,friend._area, friend._param, condition);
            AddFriend(friendRoute);
        }
    }
}