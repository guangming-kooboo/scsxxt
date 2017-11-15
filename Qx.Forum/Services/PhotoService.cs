using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace Qx.Community.Services
{
    public class PhotoService : BaseService, IPhotoService
    {
        //获取相册列表 
        public List<Photo> GetPicture(string userid)
        {
            var photo = Db.BBS_Photo.Where(a => a.UserID == userid).Select(b => new Photo()
            {
                ID=b.PhtotID,
                PhotoUrl = b.Img,
                Time = b.Time
            }).ToList();
          return photo;
      
        }
        public List<Photo> GetPicture(string userid,int count)
        {
            var photo = Db.BBS_Photo.Where(a => a.UserID == userid).Select(b => new Photo()
            {
                ID=b.PhtotID,
                PhotoUrl = b.Img,
                Time = b.Time
            }).Take(count).ToList();
            return photo;

        }
    }
}