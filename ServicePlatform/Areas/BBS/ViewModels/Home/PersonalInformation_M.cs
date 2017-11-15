using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
   public  class PersonalInformation_M
    {

       public static PersonalInformation_M ToViewModel(UserInformation userinformation,int diary,int Friend,int Photo,int Reply,int Share,int Visitor,string id)
        {
            return new PersonalInformation_M() { userinformation = userinformation, statistics = new Statistics() {  diary=diary,Friend=Friend,Photo=Photo,Share=Share,Visitor=Visitor},userid=id };
        }
        public Statistics statistics;
        public UserInformation userinformation;
        public string userid;
    }
}
