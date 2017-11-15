$(function () {

    obj = {
        //设置一个变量控制不可以多行添加
        editRow: false,
        EditWhichRow: undefined,

        //search: function () {
        //    //load方法通常可以通过传递一些参数执行一次查询，通过调用这个方法从服务器加载新数据。
        //    $('#HomePageTableBox').datagrid('load', {
        //        Newstitle: $.trim($('input[name="SearchNews"]').val()),
        //        //DateFrom: $.trim($('input[name="DateFrom"]').val()),               
        //    });
        //},

        remove: function () {
            //删除按钮
            var SelectRows = $('#specialitytablebox').datagrid('getSelections');//获取选中的要删除的行
            if (SelectRows.length > 0) {
                $.messager.confirm('确定操作', '你确定要删除选中的数据吗？', function (flag) {
                    if (flag) {
                        //alert('弹出框框');
                        var Deletedyear = [];
                        var Deletedno = [];
                        var Deleteschool = [];
                        for (var i = 0; i < SelectRows.length; i++) {
                            Deletedyear.push(SelectRows[i].EntryYear);
                            Deletedno.push(SelectRows[i].SpeNo);
                            //Deleteschool.push(SelectRows[i].SchoolID);

                        }
                        //console.log(DeletedID.join(','));

                        //使用Ajax传递需要删除的新闻编号
                        $.ajax({
                            type: 'POST',
                            url: '/Code/DeleteSpeciality',
                            data: {
                                //DeleteRow: DeletedID.join(','),
                                DeleteRow: Deletedyear,
                                DeleteRow1: Deletedno,
                              //  DeleteRow2:Deleteschool,

                            },
                            beforeSend: function () {
                                $('#specialitytablebox').datagrid('loading');
                            },
                            success: function (data) {
                                if (data != 0) {
                                    $('#specialitytablebox').datagrid('loaded');
                                    $('#specialitytablebox').datagrid('load');
                                    // $.messager.show({
                                    //   title: '提示信息',
                                    // msg: data + "个用户被删除喽",
                                    // });
                                };





                            },



                        });
                    }
                });
            }
            else {
                //给予警告
                $.messager.alert('警告', '提醒你，你没有选中要删除的行', 'warning');
            }
        },


    };



    $('#specialitytablebox').datagrid({
        url: 'GetTheDataSpe',
        width: 1300,
        title: '专业信息表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '努力加载中。。。',
        rownumbers: true,
        toolbar: '#speToolBar',

        columns: [[
            {
                field: 'CheckID',
                title: '选择框',
                width: 100,
                sortable: true,
                checkbox: true,
            },

           
            {
                field: 'EntryYear',
                title: '年级',
                width: 200,
                sortable: true,
            },
            {
                field: 'SpeNo',
                title: '专业编号',
                width: 200,
                sortable: true,
            },
            {
                field: 'SpeName',
                title: '专业名称',
                width: 200,
                sortable: true,
            },



        ]],
        pagination: true,
        pageSize: 15,
        pageList: [10, 15, 20],
        remoteSort: false,

        //双击重新编辑数据
        //onDblClickRow: function (rowIndex, rowData) {
        //if (obj.EditWhichRow != undefined) {
        //$('#HomePageTableBox').datagrid('endEdit', obj.EditWhichRow);
        //}
        //if (obj.EditWhichRow == undefined) {
        //$('#HomePageTableBox').datagrid('beginEdit', rowIndex);
        //obj.EditWhichRow = rowIndex;
        //}
        //$('#HomePageTableBox').datagrid('beginEdit', rowIndex);
        //},
    });


});
