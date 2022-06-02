$(document).ready(function () {
    $("#btn_name0").click(function () {
        var NumIID = $(this).attr('data-id');
        Generate_QR_Code(NumIID);
    });

});

function Generate_QR_Code(NumIID) {
    $.ajax({
        type: "POST",
        url: "/home/CopyDataFromMStoPQ",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            document.getElementById('MainImage').src = data;
            $("#btn_name0").prop('disabled', false);
        },
        beforeSend: function () {
            document.getElementById('MainImage').src = "/files/server_load.gif";
            $("#btn_name0").prop('disabled', true);
        },
        error: function (result) {
            console.log(result);
        },
        complete: function (endData) {

        }
    });
}