function refreshBlockChainData(_this) {
    //debugger;
    var $buttonClicked = $(_this);
    var editUrl = $buttonClicked.attr('data-url');
    var options = { "backdrop": "static", keyboard: true };

    $.ajax({
        type: "GET",
        url: editUrl,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.success == true) {
                window.location.reload();
            }
        },
        error: function () {
            //alert("Dynamic content load failed.");
        }
    });
}
$(document).ready(function () {
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

$(function () {
    $(".btnRefresh").click(function () {
        refreshBlockChainData($(this));
    });
    //auto refresh block chain status in 5 mins
    window.setTimeout(refreshBlockChainData, 5 * 60 * 1000);
});