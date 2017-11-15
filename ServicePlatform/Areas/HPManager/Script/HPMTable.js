$(function () {
    ooe = {
        //设置一个变量控制不可以多行添加
        editRow: undefined,
        CurrentRow:0,

        save: function () {
            //设置为结束编辑
            $('#HPMPageTableBox').datagrid('endEdit', CurrentRow);
            CurrentRow = 0;
        },

        change: function () {
            var Rows = $('#HPMPageTableBox').datagrid('getSelections');
            if (Rows.length == 1) {
                if (this.editRow != undefined) {
                    $('#HPMPageTableBox').datagrid('endEdit', this.editRow);
                }
                if (obj.EditWhichRow == undefined) {
                    $('#TBT2').show();
                    var CIndex = $('#HPMPageTableBox').datagrid('getRowIndex', Rows[0]);
                    CurrentRow = CIndex;
                    $('#HPMPageTableBox').datagrid('beginEdit', CIndex);
                    obj.editRow = CIndex;
                    $('#HPMPageTableBox').datagrid('unselectRow', CIndex);
                }
            }
            else {
                $.messager.alert('警告', '对不起，您只能修改一行！', 'warning');
            }
        },

    };

    $('#HPMPageTableBox').datagrid({
        url: 'GetHPMData',
        width: 1120,
        title: '栏目控制列表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '系统努力加载中。。。',
        rownumbers: true,
        toolbar: '#HPMTToolBar',

        columns: [[
            {
                field: 'CheckID',
                title: '选择框',
                width: 100,
                sortable: true,
                checkbox: true,
            },
            {
                field: 'ContentID',
                title: '内容编号',
                width: 200,
                sortable: true,
            },
           {
               field: 'ContentTypeID',
                title: '内容种类',
                width: 200,
                sortable: true,
           },
           {
               field: 'ContentTitle',
                title: '内容标题',
                width: 200,
                sortable: true,
           },
           {
               field: 'ContentPublisher',
                title: '内容发布人',
                width: 200,
                sortable: true,
           },
          {
                field: 'ManTime',
                title: '发布时间',
                width: 200,
                sortable: true,
          },
          {
                field: 'HPCSeq',
                title: '设置排序',
                width: 60,
                sortable: true,
                editor: {
                    type: 'text',
                    options: {
                    },
                },
          },
        ]],
        pagination: true,
        pageSize: 4,
        pageList: [4, 8, 12],
        remoteSort: false,

        onAfterEdit: function (rowIndex, rowData, changes) {            
            var updated = $('#HPMPageTableBox').datagrid('getChanges', 'updated');

            if (updated.length > 0) {
                //使用ajax向后台传值完成数据库的修改
                $.ajax({
                    type: 'POST',
                    url: '/HPM/EditSeq',
                    data: {
                        SeqRow: updated,
                    },
                    beforeSend: function () {
                        $('#HPMPageTableBox').datagrid('loading');
                    },
                    success: function (data) {
                        if (data) {
                            $('#HPMPageTableBox').datagrid('loaded');
                            $('#HPMPageTableBox').datagrid('load');
                            $.messager.show({
                                title: '百超提示信息',
                                msg: data + "条记录显示顺序被修改",
                            });
                        }
                    },
                });
            }

            $('#TBT2').hide();
            ooe.editRow = undefined;
        },

    });
});