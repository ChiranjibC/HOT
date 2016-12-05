$(document).ready(function () {
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

$(function () {
    $(".editLink").click(function () {
        //debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var editUrl = $buttonClicked.attr('data-url');
        var options = { "backdrop": "static", keyboard: true };

        $.ajax({
            type: "GET",
            url: editUrl,
            //contentType: "application/json; charset=utf-8",
            contentType: "text/html; charset=utf-8",
            data: { "Id": id },
            //datatype: "json",
            success: function (data) {
                //debugger;
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

});