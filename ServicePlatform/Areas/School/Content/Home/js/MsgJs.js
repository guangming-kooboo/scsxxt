﻿
$(function () {
    var pk = 1;
    $(".title td").click(function () {
        // 把要排序的内容添加到数组里
        var tIndex = parseInt($(this).index());
        var valueArray = new Array();
        var p = 0;
        for (var i = 1; i < $("table tr").length; i++) {

            if (tIndex != 0) {
                valueArray[p] = parseInt($("table tr:eq(" + i + ") td").eq(tIndex).html());
            } else {
                valueArray[p] = $("table tr:eq(" + i + ") td").eq(tIndex).html();
            }
            p++;
        }

        //数据排序
        if (pk == 1) {
            valueArray.sort(function (a, b) { return a < b ? -1 : 1 })
            pk = 2
        } else {
            valueArray.sort(function (a, b) { return a >= b ? -1 : 1 })
            pk = 1
        }

        //匹配内容        加入到一个隐藏的排序div里+-
        var dd = [];
        for (var i = 0; i < valueArray.length; i++) {
            for (var d = 1; d < $("table tr").length; d++) {
                var valueText = tIndex != 0 ? parseInt($("table tr:eq(" + d + ") td").eq(tIndex).html()) : $("table tr:eq(" + d + ") td").eq(tIndex).html();
                if (valueArray[i] == valueText && $.inArray(d, dd) < 0) {
                    dd.push(d);
                    $("table tr").eq(d).clone().appendTo(".none")
                }
            }

        }
        //重新整理html 排序，添加到视图里
        var titleClone = $("table tr").eq(0).clone(true);
        $("table").html("").append(titleClone);

        $("table").append($(".none").html())

        $(".none").html("");
    })
})

function noclic()
{
    return false;
}