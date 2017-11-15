using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class AnswerQuestion_M : BaseViewModel
    {
        public static AnswerQuestion_M ToViewModel(string uid)
        {
            return new AnswerQuestion_M()
            {
                uid = uid
            };
        }
        public string uid { get; set; }
    }
}