$(function () {
    //资源下载下载列表
    $('#DLListTable').datagrid({
        url: 'GetDLList',
        width: 1000,
        title: '资源下载',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '系统努力加载中。。。',
        rownumbers: true,

        columns: [[
            {
                field: 'CheckID',
                title: '选择框',
                width: 100,
                sortable: true,
                checkbox: true,
            },
            {
                field: 'DLFileID',
                title: '资源编号',
                width: 200,
                sortable: true,
            },
            {
                field: 'ContentTitle',
                title: '资源标题',
                width: 200,
                sortable: true,
            },
            {
                field: 'ContentPublisher',
                title: '资源发布者',
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
                field: 'ContColumnName',
                title: '发布栏目',
                width: 200,
                sortable: true,
            },
            {
                field: 'UNITID',
                title: '单位名称',
                width: 200,
                sortable: true,
            },
        ]],
        pagination: true,
        pageSize: 10,
        pageList: [10, 15, 20],
        remoteSort: false,
    });
});