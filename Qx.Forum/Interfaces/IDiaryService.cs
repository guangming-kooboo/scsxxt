using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IDiaryService
    {
        Diary GetDiary(string id);
        List<Diary> GetDiaryList(string uid);
        List<Diary> GetDiaryList(string uid,int count);
        List<Diary> GetDiaryDrafts(string id);
        List<ReplyDiary> GetReplyDiarys(string id );
    }
}