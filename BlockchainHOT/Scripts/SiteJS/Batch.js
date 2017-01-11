$(document).ready(function () {
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

$(function () {
    $(".btnRefresh").click(function () {
        //debugger;
        var $buttonClicked = $(this);
        var editUrl = $buttonClicked.attr('data-url');
        var options = { "backdrop": "static", keyboard: true };

        $.ajax({
            type: "GET",
            url: editUrl,
            contentType: "application/json; charset=utf-8",
            //contentType: "text/html; charset=utf-8",
            //data: { "Id": id },
            //datatype: "json",
            success: function (data) {
                if (data.success == true) {
                    window.location.reload();
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

});