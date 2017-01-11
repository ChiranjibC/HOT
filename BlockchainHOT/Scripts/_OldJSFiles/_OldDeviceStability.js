jQuery("#sg3").jqGrid({
    url: '/Device/GetDeviceItems',
    datatype: "json",
    //height: 190,
    width: 850,
    colNames: ['Device Id', 'Device No', 'Device Family', 'Device Name', 'LogInterval'],
    colModel: [
   		{ name: 'DeviceId', index: 'DeviceId', width: 125 },
   		{ name: 'DeviceNo', index: 'DeviceNo', width: 100 },
   		{ name: 'DeviceFamily.DeviceFamilyNo', index: 'DeviceFamily.DeviceFamilyNo', width: 150, align: "right" },
   		{ name: 'DeviceName', index: 'DeviceName', width: 120, align: "right" },
   		{ name: 'LogInterval', LogInterval: 'LogInterval', width: 80, align: "right" },
   		/*{ name: 'note', index: 'note', width: 150, sortable: false }*/
    ],
    rowNum: 10,
    rowList: [10, 20, 50],
    pager: '#psg3',
    sortname: 'DeviceId',
    viewrecords: true,
    sortorder: "desc",
    multiselect: false,
    subGrid: true,
    //caption: "Custom Icons in Subgrid",
    // define the icons in subgrid
    subGridOptions: {
        "plusicon": "ui-icon-triangle-1-e",
        "minusicon": "ui-icon-triangle-1-s",
        "openicon": "ui-icon-arrowreturn-1-e",
        // load the subgrid data only once
        // and the just show/hide
        "reloadOnExpand": false,
        // select the row when the expand column is clicked
        "selectOnExpand": true
    },
    subGridRowExpanded: function (subgrid_id, row_id) {
        var subgrid_table_id, pager_id;
        subgrid_table_id = subgrid_id + "_t";
        pager_id = "p_" + subgrid_table_id;
        $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
        jQuery("#" + subgrid_table_id).jqGrid({
            url: "subgrid.php?q=2&id=" + row_id,
            datatype: "xml",
            colNames: ['No', 'Item', 'Qty', 'Unit', 'Line Total'],
            colModel: [
				{ name: "num", index: "num", width: 80, key: true },
				{ name: "item", index: "item", width: 130 },
				{ name: "qty", index: "qty", width: 70, align: "right" },
				{ name: "unit", index: "unit", width: 70, align: "right" },
				{ name: "total", index: "total", width: 70, align: "right", sortable: false }
            ],
            rowNum: 20,
            pager: pager_id,
            sortname: 'num',
            sortorder: "asc",
            height: '100%'
        });
        jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false })
    }
});
jQuery("#sg3").jqGrid('navGrid', '#psg3', { add: true, edit: true, del: true });