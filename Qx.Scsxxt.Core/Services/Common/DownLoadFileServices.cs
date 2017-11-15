using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Core.Services.Common
{
    public class DownLoadFileServices : BaseServices, IDML<T_DownLoadFiles>
    {
        public string Add(DataContext DataContext, T_DownLoadFiles model)
        {
            throw new NotImplementedException();
        }

        public T_DownLoadFiles Find(object id)
        {
            return Db.T_DownLoadFiles.Find(id);
        }
        public List<T_DownLoadFiles> All()
        {
            return Db.T_DownLoadFiles.ToList();
        }

        public List<T_DownLoadFiles> All(DataContext DataContext, string note = "")
        {
            if (note == "该企业所有公开文件")
            {
                var UnitID = Check(DataContext, "Ent_No");
                return All(o => o.T_Content.UnitID == UnitID&&o.DLFileColumnID==3000);
            }
           else if (note == "该学校所有公开文件")
            {
                var SchoolID = Check(DataContext, "SchoolID");
                return All(o => o.T_Content.UnitID == SchoolID && o.DLFileColumnID == 2000);
            }
            else if (note == "平台所有公开文件")
            {
                return All(o => o.T_Content.UnitID == "Platform" && o.DLFileColumnID == 1000);
            }
            else if (note == "某单位的轮播广告图")
            {
                var UnitID = Check(DataContext, "UnitID");
                return All(o => o.T_Content.UnitID.Trim() == UnitID && o.DLFileColumnID == 201).
                  ToList();
            }
            else if (note == "某单位的介绍图集")
            {
                var UnitID = Check(DataContext, "UnitID");
                return All(o => o.T_Content.UnitID.Trim() == UnitID && o.DLFileColumnID == 202).
                  ToList();
            }
            else if (note == "某单位的视频集")
            {
                var UnitID = Check(DataContext, "UnitID");
                return All(o => o.T_Content.UnitID.Trim() == UnitID && o.DLFileColumnID == 203).
                  ToList();
            }
            else if (note == "某单位的PPT集")
            {
                var UnitID = Check(DataContext, "UnitID");
                return All(o => o.T_Content.UnitID.Trim() == UnitID && o.DLFileColumnID == 204).
                  ToList();
            }
            else if (note == "某单位的资源文件")
            {
                var UnitID = Check(DataContext, "UnitID");
                return All(o => o.T_Content.UnitID.Trim() == UnitID && o.DLFileColumnID == 205).
                  ToList();
            }
            else if (note == "某单位的Logo")
            {
                var UnitID = Check(DataContext, "UnitID");
                return All(o => o.T_Content.UnitID.Trim() == UnitID && o.DLFileColumnID == 206).
                  ToList();
            }
            throw new NotImplementedException();
        }

        public List<T_DownLoadFiles> All(Func<T_DownLoadFiles, bool> filter)
        {
            return Db.T_DownLoadFiles.Where(filter).ToList();
        }

        public bool Delete(DataContext DataContext, object[] id)
        {
            throw new NotImplementedException();   
        }

        public T_DownLoadFiles Find(object[] id)
        {
            throw new NotImplementedException();
        }

    

        public List<T_DownLoadFiles> All(DataContext DataContext)
        {
            return All();
        }

        public bool Delete(DataContext DataContext, object id)
        {
            Db.T_DownLoadFiles.Remove(Db.T_DownLoadFiles.Find(id));
            return Db.SaveChanges() > 0;
        }

        public bool Update(DataContext DataContext, T_DownLoadFiles model, string note)
        {
           
            throw new NotImplementedException();
        }

        public List<SelectListItem> ToSelectItems(string value="")
        {
            return All().Select(o => new SelectListItem() { Value = o.DLFileID, Text = o.DLFileUrl }).ToList().Format(value);
        }

        public List<SelectListItem> GetSelectItems<K>()
        {
            throw new NotImplementedException("联系开发人员解决");
        }


       
    }
}