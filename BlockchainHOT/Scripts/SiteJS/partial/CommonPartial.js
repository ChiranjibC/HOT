$(document).ready(function () {
    console.log("CommonPartial.js loaded");
    $("#btnClose").on("click", function () {
        console.log("btnclose");
        $('#myModal').modal('hide');
    });

    $("#editForm").submit(function (e) {
        e.preventDefault();
        $(".btnSave").prop('disabled', true);
        $(".btnSave").removeClass('btn-primary');

        console.log("ajax before send-partial");
        $('#loadingDiv').show();

        //debugger;
        console.log("From common partial -> saveBtn");
        var editForm = $(this);
        var editUrl = $(editForm).attr('action');
        var formPostType = $(editForm).attr('method');

        $.ajax({
            type: formPostType,
            url: editUrl,
            //contentType: "application/json; charset=utf-8",
            //contentType: "text/html; charset=utf-8",
            data: $(editForm).serialize(),
            //datatype: "json",
            success: function (data) {
                $('#loadingDiv').hide();
                if (data.success) {
                    window.location.reload();
                }
                else {
                    $(".errors").html(data.message);
                    $(".errors").removeClass("hidden");
                }
            },
            error: function (result) {
                alert(result);
            }
        });
    });

});