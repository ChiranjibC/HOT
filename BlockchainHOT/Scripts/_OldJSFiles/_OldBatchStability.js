//Used to populate dropdown, in case wanted the Product list to come from database
//var productList = { '1': 'Alpha', '2': 'Beta', '3': 'Gamma', '4': 'Alpha2', '5': 'Alpha3', '6': 'Beta3' };

jQuery("#tblBatch").jqGrid({
    url: '/Batch/GetBatchItems',
    datatype: "json",
    height: 'auto',
    maxHeight: 350,
    width: 850,
    colNames: ['Batch Id', 'Batch Number', 'Batch Desc', 'Product', 'Quantity'],
    colModel: [
   		{ name: 'BatchId', index: 'BatchId', width: 180, align: "center", editable: true, edittype: 'text', editoptions: { size: 80, readonly: 'readonly'}, },
   		{ name: 'BatchNumber', index: 'BatchNumber', width: 100, editable: true },
   		{ name: 'BatchDesc', index: 'BatchDesc', width: 120, editable: true },
   		{ name: 'Product', index: 'Product', width: 80, editable: true
            /*,formoptions:{rowpos:2, colpos:2}, //to move this element to existing rows new column*/
            /*, formatter: 'select',
   		        edittype: 'select',
   		        editoptions: {
   		            value: productList,
   		            dataInit: function (elem) {
   		                var v = $(elem).val();
   		                // to have short list of options which corresponds to the country
   		                // from the row we have to change temporary the column property
   		                $("#tblBatch").setColProp('Product', { editoptions: { value: productList[v] } });
   		            },
   		        },*/
   		},
   		{ name: 'Quantity', index: 'Quantity', width: 50, align: "center", sortable: false, editable: true }
    ],
    rowNum: 10,
    rowList: [10, 20, 50],
    pager: '#pgBatch',
    sortname: 'DeviceId',
    viewrecords: true,
    sortorder: "desc",
    multiselect: false,
    /*edit : {
        addCaption: "Add Record",
        editCaption: "Edit Record",
        bSubmit: "Submit",
        bCancel: "Cancel",
        bClose: "Close",
        saveData: "Data has been changed! Save changes?",
        bYes : "Yes",
        bNo : "No",
        bExit : "Cancel",
    },*/
    reloadAfterSubmit: false,
    afterSubmit: function (data, postd) {
        console.log(data);
        console.log(postd);
        return { 0: true };
    },
    afterComplete: function (data, postd) {
        return true;
    },
    editurl: '/Batch/Edit',
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
        $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll' align='center'></table><div id='" + pager_id + "' class='scroll'></div>");
        jQuery("#" + subgrid_table_id).jqGrid({
            url: "/Stability/GetStabilityDetails/BatchId=" + row_id,
            datatype: "json",
            width: 750,
            maxHeight: 250,
            colNames: ['StabilityId', 'FromTemp', 'ToTemp', 'AllowedTimeInMinutes'],
            colModel: [
				{ name: "StabilityId", index: "StabilityId", width: 200, key: true },
				{ name: "FromTemp", index: "FromTemp", width: 100, align: "center" },
				{ name: "ToTemp", index: "ToTemp", width: 100, align: "center" },
				{ name: "AllowedTimeInMinutes", index: "AllowedTimeInMinutes", width: 140, align: "center" },
            ],
            rowNum: 20,
            pager: pager_id,
            sortname: 'num',
            sortorder: "asc",
            height: '100%'
        });
        jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: true, del: true })
    }
});
jQuery("#tblBatch").jqGrid('navGrid', '#pgBatch', { add: true, edit: true, del: true }, editOptions);
//jQuery("#tblBatch").editGridRow("new", properties);
//define handler for 'editSubmit' event
var fn_editSubmit = function (response, postdata) {
    var json = response.responseText; //in my case response text form server is "{sc:true,msg:''}"
    var result = eval("(" + json + ")"); //create js object from server reponse
    return [result.sc, result.msg, null];
}

//define edit options for navgrid
var editOptions = {
    top: 50, left: "100", width: 500
 , closeOnEscape: true, afterSubmit: fn_editSubmit
}