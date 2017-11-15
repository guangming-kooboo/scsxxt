$(function () {
    //首页栏目管理下半身表格控制
    obj = {

        addToCol: function () {

            var selectHPItem = $('#SearchHPColumn').combobox('getValue');//获得上半部表格的栏目选择框数值
            //$.post("/HPM/AddToColumn", { HPItem: selectHPItem });//将上半身表格搜索框的值传入后台
            //获得选中行
            var SelectRow = $('#HPMBottomTable').datagrid('getSelections');
            var SelectID = [];
            for (var i = 0; i < SelectRow.length; i++) {
                SelectID.push(SelectRow[i].ContentID);
            }
            $.ajax({
                type: 'POST',
                url: '/HPM/AddToColumn',
                data: {
                    HPItem: selectHPItem,
                    SelecttheRow: SelectID,
                },
                beforeSend: function () {
                    $('#HPMBottomTable').datagrid('loading');
                },
                success: function (data) {
                    if (data!="0") {
                        $('#HPMBottomTable').datagrid('loaded');
                        $('#HPMBottomTable').datagrid('load');
                        $.messager.show({
                            title: '提示信息',
                            msg: data + "条内容加入栏目",
                        });
                    }
                    else if (data == "0") {
                        $('#HPMBottomTable').datagrid('loaded');
                        $('#HPMBottomTable').datagrid('load');
                        $.messager.show({
                            title: '提示信息',
                            msg: "添加失败，请确认首页栏目或内容记录处于选定状态",
                        });
                    }
                },
            });
        },
    }

    $('#HPMBottomTable').datagrid({
        url: 'GetHPMBottom',
        width: 1060,
        title: '资源管理列表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '系统努力加载中。。。',
        rownumbers: true,
        toolbar: '#HPMBToolBar',

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
               field: 'ConTypeName',
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
        ]],
        pagination: true,
        pageSize: 4,
        pageList: [4, 8, 12],
        remoteSort: false,
    });
})