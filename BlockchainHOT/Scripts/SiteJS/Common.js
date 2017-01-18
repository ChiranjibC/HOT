function enableWaitModal()
{
    var options = { "backdrop": "static", keyboard: true };
    $('#loadingModal').modal(options);
    $('#loadingModal').modal('show');
}

$(document).ready(function () {
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

$(function () {
    $.ajaxSetup({
        // your ajax code
        beforeSend: function () {
            console.log("ajax before send");
            enableWaitModal();

            //$('.modal').on('hidden.bs.modal', function (e) {
            //    if ($('.modal').hasClass('in')) {
            //        $('body').addClass('modal-open');
            //    }
            //});

            //var $mainModal = $('#myModal');
            //var $loadingModal = $("#loadingDiv");     //get reference to nested modal
            //$loadingModal.after($mainModal);
            //$mainModal.after($loadingModal);
        },
        complete: function () {
            console.log("ajax after send")
            $('#loadingModal').modal('hide');

        }
    });

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

jQuery.extend(jQuery.validator.methods, {
    range: function (value, element, param) {
        //Use the Globalization plugin to parse the value        
        var val = $.global.parseFloat(value);
        return this.optional(element) || (
            val >= param[0] && val <= param[1]);
    }
});