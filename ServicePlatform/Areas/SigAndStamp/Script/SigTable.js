$(function () {
    //资源下载管理列表
    $('#SigTableBox').datagrid({
        url: '/SSCenter/SigDataGet',
        width: 1120,
        title: '签名列表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '系统努力加载中。。。',
        rownumbers: true,
        toolbar: '#SigToolBar',

        columns: [[
            {
                field: 'CheckID',
                title: '选择框',
                width: 100,
                sortable: true,
                checkbox: true,
            },
            {
                field: 'SignatureID',
                title: '签名编号',
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
                field: 'USERID',
                title: '用户名称',
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