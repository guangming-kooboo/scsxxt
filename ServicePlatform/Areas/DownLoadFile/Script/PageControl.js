$(function () {
    $('#pp').pagination({
        total: 2000,
        //pageSize: 1
        pageList: [1, 2, 5, 10]


    });


    $('#pp').pagination({
        onSelectPage: function (pageNumber, pageSize) {
            $(this).pagination('loading');
            //alert('pageNumber:' + pageNumber + ',pageSize:' + pageSize);
            $.post("/DLList/DLTabListByEasyUi", { PageNumber: pageNumber, PageSize: pageSize });
            $(this).pagination('loaded');
        }
    });
    //var a=$('#pp').page
    //$.post("/DLList/DLTabListByEasyUi", { PageNumber: pageNumber, PageSize: pageSize });
});