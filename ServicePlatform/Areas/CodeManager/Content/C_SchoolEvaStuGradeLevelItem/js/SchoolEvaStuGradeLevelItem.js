
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
            var SelectRows = $('#SchoolEvaStuGradeLevelItemtablebox').datagrid('getSelections');//获取选中的要删除的行
            if (SelectRows.length > 0) {
                $.messager.confirm('确定操作', '你确定要删除选中的数据吗？', function (flag) {
                    if (flag) {
                        //alert('弹出框框');

                        var Deletedno = [];
                        for (var i = 0; i < SelectRows.length; i++) {

                            Deletedno.push(SelectRows[i].ItemNo);
                        }
                        //console.log(DeletedID.join(','));

                        //使用Ajax传递需要删除的新闻编号
                        $.ajax({
                            type: 'POST',
                            url: '/CodeManager/Code/DeleteSchoolEvaStuGradeLevelItem',
                            data: {
                                //DeleteRow: DeletedID.join(','),

                                DeleteRow: Deletedno,

                            },
                            beforeSend: function () {
                                $('#SchoolEvaStuGradeLevelItemtablebox').datagrid('loading');
                            },
                            success: function (data) {
                                if (data) {
                                    $('#SchoolEvaStuGradeLevelItemtablebox').datagrid('loaded');
                                    $('#SchoolEvaStuGradeLevelItemtablebox').datagrid('load');
                                    $.messager.show({
                                        title: '提示信息',
                                        msg: data + "个种类被删掉了",
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
        },


    };

    
    $('#SchoolEvaStuGradeLevelItemtablebox').datagrid({
        url: 'GetTheDataSchoolEvaStuGradeLevelItem',
        width: 1300,
        title: '学生评价学校条目等级信息表',
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
                field: 'ItemNo',
                title: '条目编号',
                width: 200,
                sortable: true,

            },
             
             {
                 field: 'ItemSequence',
                 title: '条目次序',
                 width: 200,
                 sortable: true,
             },
            {
                field: 'ItemName',
                title: '条目名称',
                width: 200,
                sortable: true,
            },
             {
                 field: 'Weight',
                 title: '条目权重',
                 width: 200,
                 sortable: true,
             },
              {
                  field: 'Note',
                  title: '条目注释',
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