$(document).ready(function () {
    $("#btn_name0").click(function () {
        var NumIID = $(this).attr('data-id');
        console.log(NumIID);
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
            console.log(data);
            document.getElementById('MainImage').src = data;
        },
        beforeSend: function () {
            document.getElementById('MainImage').src = "/files/server_load.gif";
        },
        error: function (result) {
            console.log(result);
        },
        complete: function (endData) {

        }
    });
}