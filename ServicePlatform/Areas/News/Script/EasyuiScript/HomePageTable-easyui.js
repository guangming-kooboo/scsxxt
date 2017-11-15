$(function () {

    obj = {
        //设置一个变量控制不可以多行添加
        editRow: false,
        EditWhichRow: undefined,         

        pass:function(){
            //通过审核
            var SelectRow = $('#HomePageTableBox').datagrid('getSelections');
            $.messager.confirm('确定操作', '是否审核通过？', function (flag) {
                if (flag) {
                    var SelectID = [];
                    for (var i = 0; i < SelectRow.length; i++) {
                        SelectID.push(SelectRow[i].NewsID);
                    }
                    $.ajax({
                        type: 'POST',
                        url: '/NewsCenter/StatePass',
                        data: {
                            SelecttheRow: SelectID,
                        },
                        beforeSend: function () {
                            $('#HomePageTableBox').datagrid('loading');
                        },
                        success: function (data) {
                            if (data) {
                                $('#HomePageTableBox').datagrid('loaded');
                                $('#HomePageTableBox').datagrid('load');
                                $.messager.show({
                                    title: '百超提示信息',
                                    msg: data + "个新闻审核通过",
                                });
                            }
                        },
                    });
                }
            });
        },

        reject: function () {
            //驳回
            var SelectRow = $('#HomePageTableBox').datagrid('getSelections');
            $.messager.confirm('确定操作', '是否驳回该新闻？', function (flag) {
                if (flag) {
                    var SelectID = [];
                    for (var i = 0; i < SelectRow.length; i++) {
                        SelectID.push(SelectRow[i].NewsID);
                    }
                    $.ajax({
                        type: 'POST',
                        url: '/NewsCenter/StateReject',
                        data: {
                            SelecttheRow: SelectID,
                        },
                        beforeSend: function () {
                            $('#HomePageTableBox').datagrid('loading');
                        },
                        success: function (data) {
                            if (data) {
                                $('#HomePageTableBox').datagrid('loaded');
                                $('#HomePageTableBox').datagrid('load');
                                $.messager.show({
                                    title: '百超提示信息',
                                    msg: data + "个新闻被驳回",
                                });
                            }
                        },
                    });
                }
            });
        },

        remove: function () {
            //删除按钮
            var SelectRows = $('#HomePageTableBox').datagrid('getSelections');//获取选中的要删除的行
            if (SelectRows.length > 0) {
                $.messager.confirm('确定操作', '你确定要删除选中的数据吗？', function (flag) {
                    if (flag) {
                        //alert('弹出框框');
                        var DeletedID = [];
                        for (var i = 0; i < SelectRows.length; i++) {
                            DeletedID.push(SelectRows[i].NewsID);
                        }
                        //console.log(DeletedID.join(','));

                        //使用Ajax传递需要删除的新闻编号
                        $.ajax({
                            type: 'POST',
                            url: '/NewsCenter/DeleteNews',
                            data: {
                                //DeleteRow: DeletedID.join(','),
                                DeleteRow: DeletedID,
                            },
                            beforeSend: function () {
                                $('#HomePageTableBox').datagrid('loading');
                            },
                            success: function (data) {
                                if (data) {
                                    $('#HomePageTableBox').datagrid('loaded');
                                    $('#HomePageTableBox').datagrid('load');
                                    $.messager.show({
                                        title: '百超提示信息',
                                        msg: data + "个用户被百超删除喽",
                                    });
                                }
                            },
                        });
                    }
                });
            }
            else {
                //给予警告
                $.messager.alert('警告', '百超提醒你，你没有选中要删除的行', 'warning');
            }
        },

        newshow: function () {
            //解禁功能
            var Row1 = $('#HomePageTableBox').datagrid('getSelected');
            $.messager.confirm('确定操作', '是否解禁该条新闻？', function (flag) {
                if (flag) {
                    var SelectID = Row1.NewsID;

                    $.ajax({
                        type: 'POST',
                        url: '/NewsCenter/ShowNew',
                        data: {
                            SelecttheRow: SelectID,
                        },
                        beforeSend: function () {
                            $('#HomePageTableBox').datagrid('loading');
                        },
                        success: function (data) {
                            if (data) {
                                $('#HomePageTableBox').datagrid('loaded');
                                $('#HomePageTableBox').datagrid('load');
                                $.messager.show({
                                    title: '提示信息',
                                    msg: "该条新闻已解禁",
                                });

                                $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6,#TB7,#TB8').show();
                                $('#TB8').linkbutton('disable');
                                $('#TB7').linkbutton('enable');    
                                var s = "/News/NewsCenter/HomePageTable";
                                window.location = s;
                            }
                        },
                    });
                }
            });
        },

        newhide: function () {
            //解禁功能
            var Row1 = $('#HomePageTableBox').datagrid('getSelected');
            $.messager.confirm('确定操作', '是否禁用该条新闻？', function (flag) {
                if (flag) {
                    var SelectID = Row1.NewsID;

                    $.ajax({
                        type: 'POST',
                        url: '/NewsCenter/HideNew',
                        data: {
                            SelecttheRow: SelectID,
                        },
                        beforeSend: function () {
                            $('#HomePageTableBox').datagrid('loading');
                        },
                        success: function (data) {
                            if (data) {
                                $('#HomePageTableBox').datagrid('loaded');
                                $('#HomePageTableBox').datagrid('load');
                                $.messager.show({
                                    title: '提示信息',
                                    msg: "该条新闻已禁用",
                                });

                                $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6,#TB7,#TB8').show();
                                $('#TB7').linkbutton('disable');
                                $('#TB8').linkbutton('enable');
                                var s = "/News/NewsCenter/HomePageTable";
                                window.location = s;
                            }
                        },
                    });
                }
            });
        },

        Unlock: function () {
            //解锁功能
            var Row1 = $('#HomePageTableBox').datagrid('getSelected');
            $.messager.confirm('确定操作', '是否解锁该条新闻？', function (flag) {
                if (flag) {
                    var SelectID = Row1.NewsID;

                    $.ajax({
                        type: 'POST',
                        url: '/NewsCenter/UnLockNew',
                        data: {
                            SelecttheRow: SelectID,
                        },
                        beforeSend: function () {
                            $('#HomePageTableBox').datagrid('loading');
                        },
                        success: function (data) {
                            if (data) {
                                $('#HomePageTableBox').datagrid('loaded');
                                $('#HomePageTableBox').datagrid('load');
                                $.messager.show({
                                    title: '提示信息',
                                    msg:  "该条新闻已解锁",
                                });

                                $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6').show();
                                $('#TB1').linkbutton('disable');
                                $('#TB5').linkbutton('disable');
                                $('#TB2').linkbutton('enable');
                                $('#TB3').linkbutton('enable');
                                $('#TB4').linkbutton('enable');
                                $('#TB6').linkbutton('enable');
                                var s = "/News/NewsCenter/HomePageTable";
                                window.location = s;
                            }
                        },
                    });
                }
            });
        },

        lock: function () {
            //锁定功能
            var Row1 = $('#HomePageTableBox').datagrid('getSelected');
            $.messager.confirm('确定操作', '是否锁定该条新闻？', function (flag) {
                if (flag) {
                    var SelectID = Row1.NewsID;

                    $.ajax({
                        type: 'POST',
                        url: '/NewsCenter/LockNew',
                        data: {
                            SelecttheRow: SelectID,
                        },
                        beforeSend: function () {
                            $('#HomePageTableBox').datagrid('loading');
                        },
                        success: function (data) {
                            if (data) {
                                $('#HomePageTableBox').datagrid('loaded');
                                $('#HomePageTableBox').datagrid('load');
                                $.messager.show({
                                    title: '提示信息',
                                    msg:  "该条新闻被锁定",
                                });

                                $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6').show();
                                $('#TB6').linkbutton('disable');
                                $('#TB1').linkbutton('enable');
                                $('#TB2').linkbutton('enable');
                                $('#TB3').linkbutton('enable');
                                $('#TB4').linkbutton('enable');
                                $('#TB5').linkbutton('enable');
                                var s = "/News/NewsCenter/HomePageTable";
                                window.location = s;
                            }
                        },
                    });
                }
            });
        }
    };

    $('#HomePageTableBox').datagrid({
        url: 'GetTheData',
        width: 1300,
        title: '新闻列表',
        iconCls: 'icon-search',
        nowrap: true,
        fitcolumns: false,
        loadMsg: '系统努力加载中。。。',
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
                field: 'NewsID',
                title: '新闻编号',
                width: 200,
                sortable: true,
            },
            {
                field: 'ContentTitle',
                title: '新闻标题',
                width: 200,
                sortable: true,
            },
            {
                field: 'TypeName',
                title: '新闻类型',
                width: 200,
                sortable: true,
            },
            {
                field: 'NewsAuthor',
                title: '新闻作者',
                width: 200,
                sortable: true,
            },
            {
                field: 'ContentPublisher',
                title: '新闻发布者',
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
                field: 'NewsStateName',
                title: '新闻状态',
                width: 200,
                sortable: true,
            },
            {
                field: 'IsShowName',
                title: '是否禁用',
                width: 200,
                sortable: true,
            },
            {
                field: 'IsLockName',
                title: '是否锁定',
                width: 200,
                sortable: true,
            },
            {
                field: 'ContColumnName',
                title: '新闻栏目',
                width: 200,
                sortable: true,
            },
            {
                field: 'NickName',
                title: '单位名称',
                width: 200,
                sortable: true,
            },
        ]],
        pagination: true,
        pageSize: 10,
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

        onSelect: function (rowIndex, rowData) {
            //点击行隐藏相应操作按钮
            var WeSelect1 = $('#HomePageTableBox').datagrid('getSelected');
            var lockInfo1 = WeSelect1.IsLockName;
            var showInfo1 = WeSelect1.IsShowName;
            $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6,#TB7,#TB8').show();
            if (lockInfo1 == "解锁") {
                $('#TB6').linkbutton('disable');
                $('#TB1').linkbutton('enable');
                $('#TB2').linkbutton('enable');
                $('#TB3').linkbutton('enable');
                $('#TB4').linkbutton('enable');
                $('#TB5').linkbutton('enable');
            }
            if (lockInfo1 == "锁定") {
                $('#TB1').linkbutton('disable');
                $('#TB5').linkbutton('disable');
                $('#TB2').linkbutton('enable');
                $('#TB3').linkbutton('enable');
                $('#TB4').linkbutton('enable');
                $('#TB6').linkbutton('enable');
            }
            if (showInfo1 == "不禁用") {
                $('#TB7').linkbutton('enable');
                $('#TB8').linkbutton('disable');
            }
            if (showInfo1 == "禁用") {
                $('#TB8').linkbutton('enable');
                $('#TB7').linkbutton('disable');
            }
        },

        onUnselect: function (rowIndex, rowData) {
            $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6,#TB7,#TB8').hide();
        },
        onCheck: function (rowIndex, rowData) {
            var WeSelect1 = $('#HomePageTableBox').datagrid('getSelected');
            var lockInfo1 = WeSelect1.IsLockName;
            var showInfo1 = WeSelect1.IsShowName;
            $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6,#TB7,#TB8').show();
            if (lockInfo1 == "解锁") {               
                $('#TB6').linkbutton('disable');
                $('#TB1').linkbutton('enable');
                $('#TB2').linkbutton('enable');
                $('#TB3').linkbutton('enable');
                $('#TB4').linkbutton('enable');
                $('#TB5').linkbutton('enable');
            }
            if (lockInfo1 == "锁定") {
                $('#TB1').linkbutton('disable');
                $('#TB5').linkbutton('disable');
                $('#TB2').linkbutton('enable');
                $('#TB3').linkbutton('enable');
                $('#TB4').linkbutton('enable');
                $('#TB6').linkbutton('enable');
            }
            if (showInfo1 == "不禁用") {
                $('#TB7').linkbutton('enable');
                $('#TB8').linkbutton('disable');
            }
            if (showInfo1 == "禁用") {
                $('#TB8').linkbutton('enable');
                $('#TB7').linkbutton('disable');
            }
        },
        onUncheck: function (rowIndex, rowData) {
            $('#TB1, #TB2,#TB3,#TB4,#TB5,#TB6,#TB7,#TB8').hide();
        }
    });

});