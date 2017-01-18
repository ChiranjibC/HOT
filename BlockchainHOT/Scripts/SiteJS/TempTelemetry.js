$(function () {
    $("#bulkUploadDiv").hide();
    $(".bulkUploadLink, #btnCancelUpload").click(function () {
        console.log("button clicked");
        $("#bulkUploadDiv").toggle();
    })
});