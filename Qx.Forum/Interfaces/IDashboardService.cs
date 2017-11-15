using System.Collections.Generic;

namespace Qx.Community.Interfaces
{
    public interface IDashboardService
    {
        int CalculateFriend(string userid );
        int CalculateAnser(string userid);
        int CalculateDiary(string userid);
        int CalculatePhoto(string userid);
        int CalculateShare(string userid);
        int GetDiaryReplyCount(string userid);
        int GetPostReplyCount(string userid );
        int CalculatePostCount(string userid);
        int CalculateVisitor(string userid );
        int CalculatePostDraft(string userid );
        int CalculateDiaryDraft(string userid) ;
        int CalculateMessage(string userid) ;
        int CalculateSharing(string userid) ;//进行中的分享数量
        int CalculateCancelShare(string userid );//取消分享的数量
        List<string> GetSearchKeyword();
    }
}