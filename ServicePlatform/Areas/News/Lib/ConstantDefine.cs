using ServicePlatform.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.Lib
{
    public static class ConstantDefine
    {
        /*新闻常量定义*/
        public static int STATE_WAITCHECK = 1;//新闻FlowState属性 1=>待审核
        public static int STATE_PASSCHECK = 2;//新闻FlowState属性 2=>通过
        public static int STATE_BACKCHECK = 3;//新闻FlowState属性 3=>驳回

        public static int MIX_NEWTYPE = 1;//图文新闻
        public static int PIC_NEWTYPE = 2;//图片新闻
        public static int VIDEO_NEWTYPE = 3;//视频新闻
        public static int LINK_NEWTYPE = 4;//链接新闻
        public static int DOWNLOAD_NEWTYPE = 5;//资源下载新闻

        public static int ISSHOW_SHOW = 1;//新闻IsShow属性 1=>显示
        public static int ISSHOW_HIDE = -1;//新闻IsShow属性 -1=>隐藏

        public static int ISLOCK_LOCK = 1;//新闻IsLock属性 1=>锁定
        public static int ISLOCK_NOTLOCK = -1;//新闻IsLock属性 -1=>解锁

        public static int CONTENTTYPEID = ContentType.News;//内容种类为新闻
    }
}