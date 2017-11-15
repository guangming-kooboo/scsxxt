$(function () {
    //资源下载管理列表
    $('#DLTableBox').datagrid({
        url: ' DLData',
        width: 1120,
        title: '资源下载列表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '系统努力加载中。。。',
        rownumbers: true,
        toolbar: '#DLFToolBar',

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
                field: 'UNITNAME',
                title: '单位名称',
                width: 200,
                sortable: true,
            },
        ]],
        pagination: true,
        pageSize: 50,
        pageList: [10, 15, 50],
        remoteSort: false,
    });
});