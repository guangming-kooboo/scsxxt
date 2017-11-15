
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
            var SelectRows = $('#classtablebox').datagrid('getSelections');//获取选中的要删除的行
            if (SelectRows.length > 0) {
                $.messager.confirm('确定操作', '你确定要删除选中的数据吗？', function (flag) {
                    if (flag) {
                        //alert('弹出框框');

                        var Deletedno = [];
                        for (var i = 0; i < SelectRows.length; i++) {

                            Deletedno.push(SelectRows[i].ClassID);
                        }
                        //console.log(DeletedID.join(','));

                        //使用Ajax传递需要删除的新闻编号
                        $.ajax({
                            type: 'POST',
                            url: '/CodeManager/Code/Deleteclass',
                            data: {
                                //DeleteRow: DeletedID.join(','),

                                DeleteRow: Deletedno,

                            },
                            beforeSend: function () {
                                $('#classtablebox').datagrid('loading');
                            },
                            success: function (data) {
                                if (data) {
                                    $('#classtablebox').datagrid('loaded');
                                    $('#classtablebox').datagrid('load');

                                }
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

    /*$(function () {
    
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
                var SelectRows = $('#classtablebox').datagrid('getSelections');//获取选中的要删除的行
                if (SelectRows.length > 0) {
                    $.messager.confirm('确定操作', '你确定要删除选中的数据吗？', function (flag) {
                        if (flag) {
                            //alert('弹出框框');
                            
                            var Deletedid= [];
                            for (var i = 0; i < SelectRows.length; i++) {
                                
                                Deletedid.push(SelectRows[i].ClassID);
                            }
                            //console.log(DeletedID.join(','));
    
                            //使用Ajax传递需要删除的新闻编号
                            $.ajax({
                                type: 'POST',
                                url: '/School/maintenanceClass/Deleteclass',
                                data: {
                                    //DeleteRow: DeletedID.join(','),
                                   
                                    DeleteRow: Deleteyear,
                                },
                                beforeSend: function () {
                                    $('#classtablebox').datagrid('loading');
                                },
                                success: function (data) {
                                    if (data) {
                                        $('#classtablebox').datagrid('loaded');
                                        $('#classtablebox').datagrid('load');
                                        $.messager.show({
                                            title: '提示信息',
                                            msg: data + "个用户被删除喽",
                                        });
                                    }
                                },
                            });
                        }
                    });
                }
                else {
                    //给予警告
                    $.messager.alert('警告', '提醒你，你没有选中要删除的行', 'warning');
                }
            },上面的代码段可以删除然而这个不行有空的时候看一下两者有什么区别
        };*/

    $('#classtablebox').datagrid({
        url: 'GetTheDataClass',
        width: 1300,
        title: '班级信息表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '在努力加载中。。。',
        rownumbers: true,
        toolbar: '#HomeToolBar',

        columns: [[
            {
                field: 'CheckID',
                title: '选择框',
                width: 100,
                sortable: true,
                checkbox: true,
            },

           
            {
                field: 'ClassID',
                title: '班级编号',
                width: 200,
                sortable: true,
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
            {
                field: 'ClassName',
                title: '班级名称',
                width: 230,
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