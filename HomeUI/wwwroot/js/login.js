$("#loginBtn").click(function () {    
    debugger;
    var data = {
        "username": $("#username").val(),
        "password": $("#password").val()
    }
    var url = "/Account/Login";
    var urlS = "/Home/Index";
    $.ajax({
        url: url,
        type: "post",        
        data: data,
        success: function (result) {
            if (result.success) {
                window.location.href = urlS;
            }
            else {
                Swal.fire({
                        text: result.mesaj,
                        icon: "error",
                        buttonsStyling: !1,
                        confirmButtonText: "Giriş Yapılamadı!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
            }

        },
        error: function (result) {
            Swal.fire({
                text: result.mesaj,
                icon: "error",
                buttonsStyling: !1,
                confirmButtonText: "Giriş Yapılamadı!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            })
        }

    });
});