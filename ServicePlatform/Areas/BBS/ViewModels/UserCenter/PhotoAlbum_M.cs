using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class PhotoAlbum_M
    {

        public static PhotoAlbum_M ToViewModel(List<Photo> Photos,string HeadPicture,string id)
        {
            return new PhotoAlbum_M() { Photos = Photos, HeadPicture = HeadPicture,userid=id };
        }
        public List<Photo> Photos;
        public string HeadPicture;
        public string userid;
    }
}