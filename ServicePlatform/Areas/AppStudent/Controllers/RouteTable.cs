using System;
using System.Collections.Generic;
using System.Linq;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Areas.AppStudent.Controllers
{
    public class RouteTable
    {
        private List<Route> _routeList;

        public RouteTable(List<Route> mapList)
        {
            _routeList = mapList;
        }

        public RouteTable()
        {
            _routeList = new List<Route>();
        }
        public void Regist(Route route)
        {
            _routeList.Add(route);
        }
        public string GetTarget(string currntUrl,string condition, object targetParam)
        {
            int index = currntUrl.IndexOf('?');
            if (index >0)
            {
                currntUrl = currntUrl.Substring(0, index);
            }
            var currentRoute= _routeList.FirstOrDefault(r=> currntUrl.ToLower()==(r.ID.ToLower()));
            if (currentRoute != null)
            {
                //返回当前页
                if (condition == null || !condition.HasValue() || condition == "-")
                {
                    return currentRoute.ToUrl(targetParam);
                }
                var target = condition.HasValue() ? //如果没有条件（即只有一条路径）
                    currentRoute._friends.FirstOrDefault(a => a._condition == condition):
                    currentRoute._friends.FirstOrDefault();
                if (target != null)
                {
                    return target.ToUrl(targetParam);
                }
                else
                {
                    throw new Exception("cant find target");
                }
            }
            else
            {
                throw new Exception("cant find currentRoute");
            }
         
        }
    }
}