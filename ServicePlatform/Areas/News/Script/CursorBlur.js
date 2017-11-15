$(function () {
    $("#NewsID").blur(function () {
        //新闻编号焦点离开触发事件
        var GetValue = $("#NewsID").val();
        if (GetValue == "") {
            $("#FillError1").show();
            $("#FillError2").hide();
            $("#NewsTitle").attr("disabled", "disabled");
            $("#NewsTypeID").attr("disabled", "disabled");
            $("#NewsAuthor").attr("disabled", "disabled");
            $("#NewsPublisher").attr("disabled", "disabled");
            $("#TabNewsTime").hide();
            $("#errorTime").show();
            $("#NewsColumnID").attr("disabled", "disabled");
        }
        else if (GetValue != "") {
            //输入值不为空
            $.ajax({
                type: 'POST',
                url: '/AddNews/CheckNewsID',
                data: {
                    IdValue: GetValue,
                },
                success: function (data) {
                    if (data=="存在") {
                        //通过返回值判断该条新闻编号是否在数据库中存在
                        $("#FillError1").hide();
                        $("#FillError2").show();
                        $("#NewsTitle").attr("disabled", "disabled");
                        $("#NewsTypeID").attr("disabled", "disabled");
                        $("#NewsAuthor").attr("disabled", "disabled");
                        $("#NewsPublisher").attr("disabled", "disabled");
                        $("#TabNewsTime").hide();
                        $("#errorTime").show();
                        $("#NewsColumnID").attr("disabled", "disabled");
                    }
                    if (data == "不存在") {
                        $("#FillError1").hide();
                        $("#FillError2").hide();
                        $("#NewsTitle").removeAttr("disabled");
                        $("#NewsTypeID").removeAttr("disabled");
                        $("#NewsAuthor").removeAttr("disabled");
                        $("#NewsPublisher").removeAttr("disabled");
                        $("#NewsColumnID").removeAttr("disabled");
                        $("#TabNewsTime").show();
                        $("#errorTime").hide();
                    }
                },
            });
        }
        else {
            $("#FillError1").hide();
            $("#FillError2").hide();
            $("#NewsTitle").removeAttr("disabled");
            $("#NewsTypeID").removeAttr("disabled");
            $("#NewsAuthor").removeAttr("disabled");
            $("#NewsPublisher").removeAttr("disabled");
            $("#NewsColumnID").removeAttr("disabled");
            $("#TabNewsTime").show();
            $("#errorTime").hide();
        }
    });

    //新闻标题焦点离开触发事件
    $("#NewsTitle").blur(function () {
        var GetTitleValue = $("#NewsTitle").val();
        if (GetTitleValue == "") {
            $("#FillError3").show();
            //$("#NewsID").attr("disabled", "disabled");
            $("#NewsTypeID").attr("disabled", "disabled");
            $("#NewsAuthor").attr("disabled", "disabled");
            $("#NewsPublisher").attr("disabled", "disabled");
            $("#TabNewsTime").hide();
            $("#errorTime").show();
            $("#NewsColumnID").attr("disabled", "disabled");
        }
        else {
            $("#FillError3").hide();
            //$("#NewsID").removeAttr("disabled");
            $("#NewsTypeID").removeAttr("disabled");
            $("#NewsAuthor").removeAttr("disabled");
            $("#NewsPublisher").removeAttr("disabled");
            $("#NewsColumnID").removeAttr("disabled");
            $("#TabNewsTime").show();
            $("#errorTime").hide();
        }
    });
})
