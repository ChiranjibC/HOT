$(function () {
    $(".stabilityLink").click(function () {
        //debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: StabilityDetailURL,
            //contentType: "application/json; charset=utf-8",
            contentType: "text/html; charset=utf-8",
            data: { "Id": JSON.stringify(id) },
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