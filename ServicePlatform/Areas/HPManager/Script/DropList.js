$(document).ready(function () {
    $("#SelectCType").change(function () {
        PosChange(this.value)
    });
    function PosChange(value) {
        $.ajax({
            type: "post",
            url: "GetColFromType",
            data: { S_type: value },
            dataType: "json",//这里返回的是Json数据，上面传递的也是Json类型数据
            success: function (data) {
                if (data.count > 0) {
                    $("#SelectCC option").remove();
                    $.each(data.Pos, function (i, item) {                        
                        $("#SelectCC").append("<option value='" + item.ContColumnID + "'>" + item.ContColumnName + "</option>");
                    });
                } else {
                    $("#SelectCC option").remove();
                    $("#SelectCC").append("<option value='00'>--请选择--</option>")
                }
            }
        });
    }
});