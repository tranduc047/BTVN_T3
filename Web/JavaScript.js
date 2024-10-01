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

   
    setInterval(function () { request_api_get_status_room(); }, 1000);

    $("#btnReloadCaptcha").click(function () {
        $("#captchaImage").attr("src", "api.aspx?action=capcha&" + Math.random());
    });

    $("#btnSubmit").click(function (e) {
        e.preventDefault();

        var userCaptcha = $("#txtCaptcha").val();

        // Gửi yêu cầu xác minh CAPTCHA
        $.post('api.aspx', { action: 'verifyCaptcha', captcha: userCaptcha }, function (data) {
            var result = JSON.parse(data); // Đảm bảo parse JSON

            if (result.success) {
                alert("CAPTCHA chính xác!");
                // Thực hiện hành động tiếp theo ở đây
            } else {
                alert("CAPTCHA sai, thử lại.");
                $("#captchaImage").attr("src", "api.aspx?action=capcha&" + Math.random()); // Tải lại CAPTCHA
            }
        }, 'json').fail(function (error) {
            alert("Đã xảy ra lỗi: " + error.responseText);
        });
    });
});