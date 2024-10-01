$(function () {
    function request_api_get_status_room() {
        $.post('api.aspx', { action: 'get_status' }, function (data) {
            var json = JSON.parse(data);
            json.forEach(function (room) {
                var roomElement = $('#phong' + room.ma_phong);
                if (roomElement.length) {
                    roomElement.removeClass('dangsua danghoc khonghoc');
                    switch (room.trang_thai) {
                        case 1:
                            roomElement.addClass('khonghoc');
                            break;
                        case 2:
                            roomElement.addClass('danghoc');
                            break;
                        case 3:
                            roomElement.addClass('dangsua');
                            break;
                    }
                }
            });
        });
    }

    $("#btnReloadCaptcha").click(function () {
        $("#captchaImage").attr("src", "api.aspx?action=capcha&" + Math.random());
    });

    $("#btnSubmit").click(function (e) {
        e.preventDefault();
        var userCaptcha = $("#txtCaptcha").val();

        $.post('api.aspx', { action: 'verifyCaptcha', captcha: userCaptcha }, function (data) {
            var result = JSON.parse(data);
            if (result.success) {
                alert("CAPTCHA chính xác!");
                // Thực hiện hành động tiếp theo ở đây
            } else {
                alert("CAPTCHA sai, thử lại.");
                $("#captchaImage").attr("src", "api.aspx?action=capcha&" + Math.random());
            }
        }, 'json').fail(function (error) {
            alert("Đã xảy ra lỗi: " + error.responseText);
        });
    });
});
